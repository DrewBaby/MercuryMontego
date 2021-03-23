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
        //public IActionResult Index()
        //{
        //    return View();
        //}
        // 
        // GET: /HelloWorld/Welcome/ 

        //public string Welcome()
        //{
        //    return "This is the Welcome action method...";
        //}
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




            return View("GameDetail", game);

        }

        public void SaveGame(Gamer game)
        {
             //= (int)HttpContext.Session.GetInt32("user_id");

            if (game.expansions != null)
            {
                foreach (var expansion in game.expansions)
                {
                    expansion.cover.expansion_id = expansion.id;
                    expansion.cover.gameId = expansion.id;
                }

            }

            /*
            _dbContext.VideoGameMains.Add(game);

            if (game.genres != null)
            {
                foreach (var genre in game.genres)
                {
                    if (_dbContext.VideoGameGenres.All(g => g.genre_id != genre.Id))
                    {
                        _dbContext.VideoGameGenres.Add(genre);
                    }
                    var gameGenre = new GameGenres();
                    gameGenre.gameId = game.gameId;
                    gameGenre.genre_id = genre.genre_id;
                    _dbContext.GameGenres.Add(gameGenre);

                }
            }
*/

/*            if (game.involved_companies != null)
            {
                foreach (var involComp in game.involved_companies)
                {
                    if (_dbContext.GameCompanies.All(comp => comp.company_id != involComp.company.company_id))
                    {
                        _dbContext.GameCompanies.Add(involComp.company);
                    }
                    var gameCompany = new GameCompanies();
                    gameCompany.company_id = involComp.company.company_id;
                    gameCompany.gameId = game.gameId;
                    _dbContext.GameCompanies.Add(gameCompany);

                }
            }

 */
            if (game.platforms != null)
            {
                foreach (var platform in game.platforms)
                {
                    //if (_dbContext.VideoGamePlatforms.All(p => p.PKey != platform.platform_id))
                    //{
                    //    _dbContext.VideoGamePlatforms.Add(platform);
                    //}
                    //var gamePlatform = new GamePlatforms();
                    //gamePlatform.gameId = game.gameId;
                    //gamePlatform.platform_id = platform.platform_id;
                    //_dbContext.VideoGamePlatforms.Add(gamePlatform);


                }
            }

            _dbContext.SaveChanges();

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
        public IActionResult GamesOnRating(RatingForm rating_form)
        {

            HttpContext.Session.SetInt32("MaxRating", (int)rating_form.MaxRating);
            HttpContext.Session.SetInt32("MinRating", (int)rating_form.MinRating);

            Console.WriteLine(rating_form.MaxRating + "," + rating_form.MinRating);

            return RedirectToAction("Home");
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