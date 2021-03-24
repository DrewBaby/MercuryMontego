using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using IGDB;
using IGDB.Models;
using GameClubProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace GameClubProject.Controllers
{
    [Authorize]
    public class GameQueryController : Controller
    {
        private protected string IGDB_CLIENT_ID = "suir6rwr94fhs2s3x8jcyfqfjpd6lj";
        private protected string IGDB_CLIENT_SECRET = "a07fcbs0o7est8f2p9o6qpk09t5zfw";     
        private protected IGDBClient _api;
        private readonly GameclubDBContext _dbContext;

        public GameQueryController(GameclubDBContext context)
        {
            _dbContext = context;
        }

        //Retrieves default content based on Search criteria. 
        public async Task<DashboardGameBundle> FetchGames(string gameName = null)
        {
            ViewBag.MaxRating = (int)HttpContext.Session.GetInt32("MaxRating");
            ViewBag.MinRating = (int)HttpContext.Session.GetInt32("MinRating");

            var games = new List<Game>();
            if (gameName != null)
            {
                games = await GameDBLogic.LoadGamesBasedOnSearch(gameName);
            }
            else
            {
                games = await GameDBLogic.LoadGamesBasedOnFilters(MinRating: ViewBag.MinRating, MaxRating: ViewBag.MaxRating, genre: HttpContext.Session.GetString("Genre"), platform: HttpContext.Session.GetString("Platform"));
            }
            var DashboardBundle = new DashboardGameBundle();

            if (games.Count > 0)
            {
                DashboardBundle.popularGames = await GameDBLogic.LoadPopularGames();
                DashboardBundle.genreAGames = await GameDBLogic.LoadGenreAGames();
                DashboardBundle.genreBGames = await GameDBLogic.LoadGenreBGames();
                DashboardBundle.genreCGames = await GameDBLogic.LoadGenreCGames();

            }
            DashboardBundle.games = games;

            return DashboardBundle;
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public string Welcome(string name, int numTimes = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
        }

        
        public async Task<IActionResult> HomeAsync()
        {
            
            var userid = User.Claims.FirstOrDefault().Value.ToString();
            if (userid == null) { return RedirectToAction("login", "LogonProfile"); }
            _api = new IGDB.IGDBClient(IGDB_CLIENT_ID, IGDB_CLIENT_SECRET);
            int? Maxrating = HttpContext.Session.GetInt32("MaxRating");
            int? Minrating = HttpContext.Session.GetInt32("MinRating");
            if (Maxrating == null)
            {
                HttpContext.Session.SetInt32("MaxRating", 100);
            }
            if (Minrating == null)
            {
                HttpContext.Session.SetInt32("MinRating", 1);
            }
            
            if (TempData["gameName"] == null)
            {
                TempData["gameName"] = "Halo";   //Test Account
            }
            var DashboardBundle = await FetchGames((string)TempData["gameName"]);
            if (DashboardBundle.games.Count < 1)
            {
                TempData["gameName"] = "Halo";
                return RedirectToAction("ForbiddenError", "ErrorPages");
            }
            var dbUser=_dbContext.UserAccounts.FirstOrDefault(user => user.UserId == userid);

            if (dbUser == null) { return RedirectToAction("register", "LogonProfile"); }
                //throw new NullReferenceException(nameof(dbUser));

                DashboardBundle.user = dbUser;

            HttpContext.Session.SetString("userName", DashboardBundle.user.FirstName + " " + DashboardBundle.user.LastName);

            ViewBag.filters = new Dictionary<string, object> {
                {"MinRating",(int)HttpContext.Session.GetInt32("MinRating")},
                {"MaxRating",(int)HttpContext.Session.GetInt32("MaxRating")},
                {"Platform",HttpContext.Session.GetString("Platform")},
                {"Genre",HttpContext.Session.GetString("Genre")}
            };

            ViewBag.userName = HttpContext.Session.GetString("userName");

            return View("Dashboard", DashboardBundle);       
           
        }


        [HttpPost("GameSearch")]
        public async Task<IActionResult> GameSearch(string gameName)
        {
            if (ModelState.IsValid)
            {
                TempData["gameName"] = gameName;
                return RedirectToAction("Home");
            }

            var DashboardBundle = await FetchGames();
            ViewBag.filters = new Dictionary<string, object> {
                {"MinRating",(int)HttpContext.Session.GetInt32("MinRating")},
                {"MaxRating",(int)HttpContext.Session.GetInt32("MaxRating")},
                {"Platform",HttpContext.Session.GetString("Platform")},
                {"Genre",HttpContext.Session.GetString("Genre")}

            };            
            return View("Dashboard", DashboardBundle);
        }


        [HttpGet("ShowGame/{gameId}")]
        public async Task<IActionResult> ShowGame(int gameId)
        {
            var gamePageDetail = new GamePageDetail();
            Game game = await GameDBLogic.ShowOneGame(gameId);
            //SaveGame(game);
            //gamePageDetail.game = await GameDBLogic.ShowOneGame(gameId);            
            ViewBag.gameID = gameId.ToString();

            return View("GameDetail", game);

        }



        [HttpPost("SaveGame")]
        public IActionResult SaveGame(Game game)
        {
            var videoGameUserContent = new VideoGameUserContent();
            var videoGameMain = new VideoGameMain();
            videoGameMain.GameId= game.Id.ToString();
            videoGameUserContent.UserId = User.Claims.FirstOrDefault().Value.ToString();            
            if (videoGameUserContent.UserId != null)
            {
                videoGameUserContent.GameId = game.Id.ToString();
                videoGameUserContent.UserRating = 2;
                videoGameUserContent.UserReview = "Space for rent";
                videoGameMain.VideoGameUserContents.Add(videoGameUserContent);
                if (_dbContext.VideoGameMains.All(g => g.GameId != videoGameMain.GameId))
                {
                    _dbContext.VideoGameMains.Add(videoGameMain);
                    _dbContext.SaveChanges();
                }
            }          

            return RedirectToAction("Home");


        }

        [HttpPost("SaveReview")]
        public IActionResult SaveReview(ReviewModel game)
        {
            var userid = User.Claims.FirstOrDefault().Value.ToString();
            // && a.UserId == User.Claims.FirstOrDefault().Value.ToString())
            if (_dbContext.VideoGameUserContents.All(a => a.GameId == game.GameID))
            {
                
                var reviewUpdate = _dbContext.VideoGameUserContents.First(a => a.GameId == game.GameID && a.UserId == userid);
                reviewUpdate.UserReview = game.UserReview;
                _dbContext.SaveChanges();
            }
            else
            {

            }

            return RedirectToAction("Home");


        }





        [HttpGet("fetchGenres")]
        public async Task<IActionResult> FetchGenres()
        {

            var filters = await GameDBLogic.FetchGenres();

            return Json(filters.genres);
        }

        [HttpGet("advance_search")]
        public async Task<IActionResult> AdvanceSearch()
        {
            if (ModelState.IsValid)
            {
                //TempData["gameName"] = gameName;
                //return RedirectToAction("Home");
            }

            var DashboardBundle = await FetchGames();
            ViewBag.filters = new Dictionary<string, object> {
                {"MinRating",(int)HttpContext.Session.GetInt32("MinRating")},
                {"MaxRating",(int)HttpContext.Session.GetInt32("MaxRating")},
                {"Platform",HttpContext.Session.GetString("Platform")},
                {"Genre",HttpContext.Session.GetString("Genre")}

            };
            return View("Dashboard", DashboardBundle);
        }

        [HttpGet("fetchPlatforms")]
        public IActionResult FetchPlatforms()
        {

            var filters = new Filters();

            return Json(filters.platforms);
        }

        [HttpPost("GamesOnRating")]
        public async Task<IActionResult> GamesOnRating(RatingForm rating_form)
        {
            HttpContext.Session.SetInt32("MaxRating", (int)rating_form.MaxRating);
            HttpContext.Session.SetInt32("MinRating", (int)rating_form.MinRating);
            var DashboardBundle = await FetchGames();
            ViewBag.filters = new Dictionary<string, object> {
                {"MinRating",(int)HttpContext.Session.GetInt32("MinRating")},
                {"MaxRating",(int)HttpContext.Session.GetInt32("MaxRating")},
                {"Platform",HttpContext.Session.GetString("Platform")},
                {"Genre",HttpContext.Session.GetString("Genre")}
            };
            ViewBag.userName = HttpContext.Session.GetString("userName");
            return View("Dashboard", DashboardBundle);            
        }

        [HttpGet("GameOnGenres/{genre}")]
        public async Task<IActionResult> GameOnGenres(string genre)
        {
            HttpContext.Session.SetString("Genre", genre);
            var DashboardBundle = await FetchGames();
            ViewBag.filters = new Dictionary<string, object> {
                {"MinRating",(int)HttpContext.Session.GetInt32("MinRating")},
                {"MaxRating",(int)HttpContext.Session.GetInt32("MaxRating")},
                {"Platform",HttpContext.Session.GetString("Platform")},
                {"Genre",HttpContext.Session.GetString("Genre")}
            };
            ViewBag.userName = HttpContext.Session.GetString("userName");
            return View("Dashboard", DashboardBundle);

        }

        [HttpGet("GameOnPlatform/{platform}")]
        public async Task<IActionResult> GameOnPlatform(string platform)
        {
            HttpContext.Session.SetString("Platform", platform);
            var DashboardBundle = await FetchGames();
            ViewBag.filters = new Dictionary<string, object> {
                {"MinRating",(int)HttpContext.Session.GetInt32("MinRating")},
                {"MaxRating",(int)HttpContext.Session.GetInt32("MaxRating")},
                {"Platform",HttpContext.Session.GetString("Platform")},
                {"Genre",HttpContext.Session.GetString("Genre")}
            };
            ViewBag.userName = HttpContext.Session.GetString("userName");
            return View("Dashboard", DashboardBundle);
        }
    }     
}