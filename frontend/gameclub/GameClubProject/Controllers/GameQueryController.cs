﻿using System;
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

        
        public async Task<IActionResult> HomeAsync()
        {
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
            ViewBag.filters = new Dictionary<string, object> {
                {"MinRating",(int)HttpContext.Session.GetInt32("MinRating")},
                {"MaxRating",(int)HttpContext.Session.GetInt32("MaxRating")},
                {"Platform",HttpContext.Session.GetString("Platform")},
                {"Genre",HttpContext.Session.GetString("Genre")}
            };

            return View("Dashboard", DashboardBundle);       
           // return View(games);
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
            //gameProfileBundle.user = dbContext.Users.FirstOrDefault(user => user.user_id == user_id);
            //if (dbContext.Games.FirstOrDefault(game => game.gameId == gameId) == null)
            //{
            gamePageDetail.game = await GameDBLogic.ShowOneGame(gameId);
            //SaveGame(game);
            //}
            /*
            gameProfileBundle.game = dbContext.Games.Include(g => g.cover)
                                                .Include(g => g.game_Genres).ThenInclude(g => g.genre)
                                                .Include(g => g.game_Companies).ThenInclude(invc => invc.company)
                                                .Include(g => g.game_Platforms).ThenInclude(p => p.platform)
                                                .Include(g => g.screenshots)
                                                .Include(g => g.videos)
                                                .Include(g => g.expansions).ThenInclude(e => e.cover)
                                                .Include(g => g.Reviews).ThenInclude(r => r.reviewer)
                                                .Include(g => g.Reviews).ThenInclude(r => r.likeCounts)
                                                .Include(g => g.Reviews).ThenInclude(r => r.responses)
                                                .FirstOrDefault(g => g.gameId == gameId);

            
            gameProfileBundle.formReview = new Review();
            gameProfileBundle.Comment = new ReviewResponse();
            ViewBag.userName = HttpContext.Session.GetString("userName");
            */
            return View("GameDetail", gamePageDetail);

        }


    }
}




