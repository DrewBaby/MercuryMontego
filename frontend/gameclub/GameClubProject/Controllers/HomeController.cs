using GameClubProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using IGDB.Models;
using IGDB;
using System.Linq;
using System.Threading.Tasks;

namespace GameClubProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //IGDBClient _api;
  
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            //var IGDB_CLIENT_ID = "suir6rwr94fhs2s3x8jcyfqfjpd6lj";
            //var IGDB_CLIENT_SECRET = "a07fcbs0o7est8f2p9o6qpk09t5zfw";

            ////_api = new IGDB.IGDBClient(
            ////  Environment.GetEnvironmentVariable("IGDB_CLIENT_ID"),
            ////  Environment.GetEnvironmentVariable("IGDB_CLIENT_SECRET")
            ////);
            //_api = new IGDB.IGDBClient(IGDB_CLIENT_ID, IGDB_CLIENT_SECRET);

        }

        public IActionResult Index()
        {
            
            // async Task ShouldReturnResponseWithoutQuery()
            //{
            //    var games = await _api.QueryAsync<Game>(IGDBClient.Endpoints.Games, query: "search 'Halo'; fields id,name,summary;");
            //    var game = games.First();
            //    ViewBag.Message = game.Id;
            //    Console.WriteLine(game.Id);
            //    Console.WriteLine(game.Name);

            //}

         

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
