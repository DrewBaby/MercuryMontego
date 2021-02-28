using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using IGDB;
using IGDB.Models;

namespace GameClubProject.Controllers
{
    public class GameQueryController : Controller
    {
        // 
        // GET: /HelloWorld/

        //public string Index()
        //{
        //    return "This is my default action...";
        //}

        private protected string IGDB_CLIENT_ID = "suir6rwr94fhs2s3x8jcyfqfjpd6lj";
        private protected string IGDB_CLIENT_SECRET = "a07fcbs0o7est8f2p9o6qpk09t5zfw";     
        private protected IGDBClient _api;

        //private readonly MvcMovieContext _context;

        //public MoviesController(MvcMovieContext context)
        //{
        //    _context = context;
        //}

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

    }
}



//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace GameClubProject.Controllers
//{
//    public class GameQueryController : Controller
//    {
//        public IActionResult Index()
//        {
//            return View();
//        }
//    }
//}
