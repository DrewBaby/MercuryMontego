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
                    if (_dbContext.VideoGamePlatforms.All(p => p.PKey != platform.platform_id))
                    {
                        _dbContext.VideoGamePlatforms.Add(platform);
                    }
                    var gamePlatform = new GamePlatforms();
                    gamePlatform.gameId = game.gameId;
                    gamePlatform.platform_id = platform.platform_id;
                    _dbContext.VideoGamePlatforms.Add(gamePlatform);


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


        // Added Travis section to this code    
        // I am adusting it to to compensate in the difference in changes made to the _api.QueryAsync method.
        // --- List of views go here ---
        #region Travis Simmons Addition - 03/13/2021
        public async Task<IActionResult> IGDBQueryAsync()
        {

            _api = new IGDB.IGDBClient(IGDB_CLIENT_ID, IGDB_CLIENT_SECRET);
            var games = await _api.QueryAsync<Game>(IGDBClient.Endpoints.Games, query: "fields *; limit 50;");

            //foreach (var game in games)
            //{
            //    game.Id(mbox => mbox.ID == id);
            //    _ = game.Name; // Thief
            //}

            //ViewData [] = "Hello " + name;
            //ViewData["NumTimes"] = numTimes;

            return View(games);
        }

        
        public async Task<IActionResult> IndexAsync()
        {
            const int REEL_LENGTH = 15;

            _api = new IGDB.IGDBClient(IGDB_CLIENT_ID, IGDB_CLIENT_SECRET);
            IndexReel model = new IndexReel();
            string coverImageIDSet;
            Random random = new Random();
            long?[] genreId = new long?[2];

            // Select 2 genres at random from the IGDB Genre table
            var listOfGenres = await _api.QueryAsync<Genre>(IGDBClient.Endpoints.Genres, query: "fields *; limit 50; sort id asc;");
            List<long?> genreList = new List<long?>();
            foreach (Genre gameGenre in listOfGenres)
            {
                genreList.Add(gameGenre.Id);
            }
            do
            {
                int index = random.Next(genreList.Count);
                if (genreId[0] == null) genreId[0] = genreList[index];
                if (genreList[index] != genreId[0]) genreId[1] = genreList[index];
            } while (genreId[0] == null || genreId[1] == null);
            // Pass array of 2 selected genres to api queries to get the top 10 games
            // for that genre and the associated cover image details
            for (int x = 0; x < 2; x++)
            {
                var gamesByGenre = await _api.QueryAsync<Game>(IGDBClient.Endpoints.Games, query: "fields id, name, cover, rating; where cover != null & rating != null & genres = (" + genreId[x] + "); sort rating desc; limit " + REEL_LENGTH + ";");
                coverImageIDSet = GenerateCoverImageIDSet(gamesByGenre);
                var gameCoverByGenre = await _api.QueryAsync<IGDB.Models.Cover>(IGDBClient.Endpoints.Covers, query: "fields alpha_channel, animated, checksum, game, height, image_id, url, width; where id = (" + coverImageIDSet + "); limit " + REEL_LENGTH + ";");
                gameCoverByGenre = ReorderCoverImageArrayByGameArray(gamesByGenre, gameCoverByGenre);
                if (x == 0)
                {
                    model.GenreATitle = listOfGenres[Array.IndexOf(genreList.ToArray(), genreId[x])].Name;
                    model.GenreAGames = MergeDataSets(gamesByGenre, gameCoverByGenre);
                }
                if (x == 1)
                {
                    model.GenreBTitle = listOfGenres[Array.IndexOf(genreList.ToArray(), genreId[x])].Name;
                    model.GenreBGames = MergeDataSets(gamesByGenre, gameCoverByGenre);
                }
            }

            // Get 10 games with highest ratings and their cover pictures
            var topGames = await _api.QueryAsync<Game>(IGDBClient.Endpoints.Games, query: "fields id, name, cover, genres, rating; where cover != null & rating != null; sort rating desc; limit " + REEL_LENGTH + ";");
            coverImageIDSet = GenerateCoverImageIDSet(topGames);
            var covers = await _api.QueryAsync<Cover>(IGDBClient.Endpoints.Covers, query: "fields alpha_channel, animated, checksum, game, height, image_id, url, width; where id = (" + coverImageIDSet + "); limit " + REEL_LENGTH + ";");
            covers = ReorderCoverImageArrayByGameArray(topGames, covers);
            model.TopRatedGames = MergeDataSets(topGames, covers);

            /*
            Example games: 10 games with the highest ratings
            Example covers: covers associated with the 10 games above
             Example coverImageIDSet: 71, 84491, 97021, 111232, 117024, 43615, 54330, 83799, 98723, 81698
             Example data returned for image api call:
             id = 71
             game = 70 :Ref ID for gameID
             url = //images.igdb.com/igdb/image/upload/t_thumb/ndfzbf3xvuuchijx7v1c.jpg
             height = 347
             width = 288
             alphaChannel = false
             animated = false
             checksum = 144656d4 - 5cfd - 86a4 - d785 - 89f7fcb8bb96

             Returns model which consists of 3 sets of games and their associated cover images
             TopGames = top rated games
             GenreAGames & GenreBGames = top rated games by genre(genres chosen at random on page load)
            */
            return View(model);
            //return View(covers);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            _api = new IGDB.IGDBClient(IGDB_CLIENT_ID, IGDB_CLIENT_SECRET);
            var game = await _api.QueryAsync<Game>(IGDBClient.Endpoints.Games, query: "fields *; where id =" + id + "; limit 1;");
            Game gameDetail = game[0];
            return View(gameDetail);
        }       


        // Helper methods 
        //
        // Generates a concatenated string of IDs for cover images associated with the games passed to the method
        // Example result set: 71, 84491, 97021, 111232, 117024, 43615, 54330, 83799, 98723, 81698
        private string GenerateCoverImageIDSet(List<Game> gameSet)
        {
            string coverImageIDSet = "";
            for (int x = 0; x < gameSet.Count; x++)
            {
                long? CoverID = gameSet[x].Cover.Id;
                if (x < gameSet.Count - 1)
                {
                    coverImageIDSet = coverImageIDSet + CoverID + ", ";
                }
                else
                {
                    coverImageIDSet = coverImageIDSet + CoverID;
                }
            }
            return coverImageIDSet;
        }

        // Reorders an array of Cover so that a game in Game[] will accurately 
        // associate it's cover image details in Cover[]. 
        private List<IGDB.Models.Cover> ReorderCoverImageArrayByGameArray(List<Game> arrayToMatch, List<IGDB.Models.Cover> arrayReorder)
        {
            var arrayOrdered = new IGDB.Models.Cover[arrayReorder.Count];
            long?[] searchArray = new long?[arrayReorder.Count];
            for (int x = 0; x < searchArray.Length; x++)
            {
                searchArray[x] = arrayReorder[x].Game.Id;
            }
            for (int x = 0; x < arrayToMatch.Count; x++)
            {
                long? indexToFind = arrayToMatch[x].Id;
                long? index;
                index = Array.IndexOf(searchArray, indexToFind);
                int indexInt = Convert.ToInt32(index);
                arrayOrdered[x] = arrayReorder[indexInt];
            }

            List<IGDB.Models.Cover> lstArrayOrdered = arrayOrdered.ToList();
            return lstArrayOrdered;
        }

        // Nice generates an array of ReelItems that consist of a video game and it's associated
        // cover image details
        private List<ReelItem> MergeDataSets(List<Game> game, List<IGDB.Models.Cover> cover)
        {
            var gameReel = new ReelItem[game.Count];
            for (int y = 0; y < game.Count; y++)
            {
                var item = new ReelItem(game[y], cover[y]);
                gameReel[y] = item;
            }
            //Andrew Lawson - Added to convert from an array to a List for ease of management 
            List<ReelItem> lstgameReel = gameReel.ToList();
            return lstgameReel;
        }

        #endregion


    }
}




