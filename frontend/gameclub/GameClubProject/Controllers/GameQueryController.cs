using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Text;
using IGDB;
using IGDB.Models;
using GameClubProject.Models;
using Microsoft.AspNetCore.Http;

namespace GameClubProject.Controllers
{
    public class GameQueryController : Controller
    {
        

        private protected string IGDB_CLIENT_ID = "suir6rwr94fhs2s3x8jcyfqfjpd6lj";
        private protected string IGDB_CLIENT_SECRET = "a07fcbs0o7est8f2p9o6qpk09t5zfw";     
        private protected IGDBClient _api;


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



        public async Task<List<Genre>> FetchGenres()
        {           
            var _api = new IGDB.IGDBClient(IGDB_CLIENT_ID, IGDB_CLIENT_SECRET);
            //var games = await _api.QueryAsync<Game>(IGDBClient.Endpoints.Games, query: "fields *; limit 50;");        
            var query = (@"fields name,slug; limit 20; ");
            try
            {                
                var genres = await _api.QueryAsync<Genre>(IGDBClient.Endpoints.Genres, query);                
                var filters = new Filters();
                //filters.genres = genres;
                return genres;
            }
            catch
            {
                throw new Exception("error");
            }

        }

        public async Task<List<Game>> LoadGamesBasedOnSearch(string name)
        {

            string IGDB_CLIENT_ID = "suir6rwr94fhs2s3x8jcyfqfjpd6lj";
            string IGDB_CLIENT_SECRET = "a07fcbs0o7est8f2p9o6qpk09t5zfw";
            var _api = new IGDB.IGDBClient(IGDB_CLIENT_ID, IGDB_CLIENT_SECRET);
            var query = $@"
                fields genres.name,genres.slug,cover.url,cover.image_id,name,rating,first_release_date,platforms.name;
                where popularity>1&rating>1;
                search ""{name}"";
                limit 20;";
            try
            {
                var games = await _api.QueryAsync<Game>(IGDBClient.Endpoints.Games, query);
                System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                foreach (var game in games)
                {
                    dateTime = dateTime.AddSeconds(Convert.ToSingle(game.FirstReleaseDate));
                    game.FirstReleaseDate = dateTime;
                }
                return games;
            }
            catch
            {
                throw new Exception("error");
            }


        }


        public IActionResult Index()
        {
            return View();
        }
        // 
        // GET: /HelloWorld/Welcome/ 

        //public string Welcome()
        //{
        //    return "This is the Welcome action method...";
        //}

        public string Welcome(string name, int numTimes = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
        }

        public async Task<IActionResult> IGDBQueryAsync()
        {

            _api = new IGDB.IGDBClient(IGDB_CLIENT_ID, IGDB_CLIENT_SECRET);
            //var games = await _api.QueryAsync<Game>(IGDBClient.Endpoints.Games, "fields *; limit 50;");
            string genre = null;
            string platform = null;
            int MinRating = 1;
            int MaxRating = 100;


            var gamer= FetchGenres();
            //var query = GameDBLogic.queryString(MinRating, MaxRating, genre: genre, platform: platform);
            var games = await _api.QueryAsync<Game>(IGDBClient.Endpoints.Games, "fields *; limit 50;");
            // var games = await _api.QueryAsyncs<Game>(IGDBClient.Endpoints.Games, new StringContent(query));
            //foreach (var game in games)
            //{
            //    g // Thief
            //}

            ////ViewData [] = "Hello " + name;
            ////ViewData["NumTimes"] = numTimes;
            //TempData["gameName"] = "Halo";
            //HttpContext.Session.SetInt32("MinRating", 1);
            //HttpContext.Session.SetInt32("MaxRating", 100);

            //int? Maxrating = HttpContext.Session.GetInt32("MaxRating");
            //int? Minrating = HttpContext.Session.GetInt32("MinRating");
            //if (Maxrating == null)
            //{
            //    HttpContext.Session.SetInt32("MaxRating", 100);
            //}
            //if (Minrating == null)
            //{
            //    HttpContext.Session.SetInt32("MinRating", 1);
            //}
            //var DashboardBundle = await FetchGames((string)TempData["gameName"]);
            //if (DashboardBundle.games.Count < 1)
            //{
            //    return RedirectToAction("ForbiddenPage", "ErrorPage");
            //}
            //ViewBag.filters = new Dictionary<string, object> {
            //    {"MinRating",(int)HttpContext.Session.GetInt32("MinRating")},
            //    {"MaxRating",(int)HttpContext.Session.GetInt32("MaxRating")},
            //    {"Platform",HttpContext.Session.GetString("Platform")},
            //    {"Genre",HttpContext.Session.GetString("Genre")}

            //};



            //return View("Dashboard",DashboardBundle);
            return View(games);
        }


    }
}




