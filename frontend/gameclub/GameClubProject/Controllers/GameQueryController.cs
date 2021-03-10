using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using IGDB;
using IGDB.Models;
using System.Collections.Generic;
using System;

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

        // --- List of views go here ---
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
            _api = new IGDB.IGDBClient(IGDB_CLIENT_ID, IGDB_CLIENT_SECRET);
            IndexViewModel model = new IndexViewModel();
            string coverImageIDSet;
            Random random = new Random();
            long?[] genreId = new long?[2];

            // Select 2 genres at random from the IGDB Genre table
            Genre[] listOfGenres = await _api.QueryAsync<Genre>(IGDBClient.Endpoints.Genres, query: "fields *; limit 50; sort id asc;");
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
                Game[] gamesByGenre = await _api.QueryAsync<Game>(IGDBClient.Endpoints.Games, query: "fields id, name, cover, rating; where cover != null & rating != null & genres = (" + genreId[x] + "); sort rating desc; limit 10;");
                coverImageIDSet = GenerateCoverImageIDSet(gamesByGenre);
                Cover[] gameCoverByGenre = await _api.QueryAsync<Cover>(IGDBClient.Endpoints.Covers, query: "fields alpha_channel, animated, checksum, game, height, image_id, url, width; where id = (" + coverImageIDSet + ");");
                gameCoverByGenre = ReorderCoverImageArrayByGameArray(gamesByGenre, gameCoverByGenre);
                if (x == 0)
                {
                    model.GameCollectionB = gamesByGenre;
                    model.CoverCollectionB = gameCoverByGenre;
                }
                if (x == 1)
                {
                    model.GameCollectionC = gamesByGenre;
                    model.CoverCollectionC = gameCoverByGenre;
                }
            }

            // Get 10 games with highest ratings and their cover pictures
            Game[] topTenGames = await _api.QueryAsync<Game>(IGDBClient.Endpoints.Games, query: "fields id, name, cover, genres, rating; where cover != null & rating != null; sort rating desc; limit 10;");
            model.GameCollectionA = topTenGames;
            coverImageIDSet = GenerateCoverImageIDSet(topTenGames);
            Cover[] covers = await _api.QueryAsync<Cover>(IGDBClient.Endpoints.Covers, query: "fields alpha_channel, animated, checksum, game, height, image_id, url, width; where id = ("+ coverImageIDSet +");");
            covers = ReorderCoverImageArrayByGameArray(topTenGames, covers);
            model.CoverCollectionA = covers;

            // Example games: 10 games with the highest ratings
            // Example covers: covers associated with the 10 games above
            // Example coverImageIDSet: 71, 84491, 97021, 111232, 117024, 43615, 54330, 83799, 98723, 81698
            // Example data returned for image api call:
            // id = 71
            // game = 70 :Ref ID for gameID
            // url = //images.igdb.com/igdb/image/upload/t_thumb/ndfzbf3xvuuchijx7v1c.jpg
            // height = 347
            // width = 288
            // alphaChannel = false
            // animated = false
            // checksum = 144656d4-5cfd-86a4-d785-89f7fcb8bb96

            // Returns model which consists of 3 sets of games and their associated cover images
            // CollectionA = top 10 games by rating
            // CollectionB & C = top 10 games by genre (genres chosen at random on page load)
            //return View(model)
            return View(covers);
        }
        public IActionResult About()
        {
            return View();
        }

        public string Welcome(string name, int numTimes = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
        }

        // Helper methods 
        //
        // Generates a concatenated string of IDs for cover images associated with the games passed to the method
        // Example result set: 71, 84491, 97021, 111232, 117024, 43615, 54330, 83799, 98723, 81698
        private string GenerateCoverImageIDSet(Game[] gameSet)
        {
            string coverImageIDSet = "";
            for (int x = 0; x < gameSet.Length; x++)
            {
                long? CoverID = gameSet[x].Cover.Id;
                if (x < gameSet.Length - 1)
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

        // Reorders an array of Cover so that a game at position game[x] will accurately associate it's cover
        // image details in cover[x]. Enables it so when calling GameCollectionA[1], you can find the
        // associated cover image details in CoverCollectionA[1] when displaying data on pages.
        private Cover[] ReorderCoverImageArrayByGameArray(Game[] arrayToMatch, Cover[] arrayReorder)
        {
            Cover[] arrayOrdered = new Cover[arrayReorder.Length];
            long?[] searchArray = new long?[arrayReorder.Length];
            for (int x = 0; x < searchArray.Length; x++)
            {
                searchArray[x] = arrayReorder[x].Game.Id;
            }
            for (int x = 0; x < arrayToMatch.Length; x++)
            {
                long? indexToFind = arrayToMatch[x].Id;
                long? index;
                index = Array.IndexOf(searchArray, indexToFind);
                int indexInt = Convert.ToInt32(index);
                arrayOrdered[x] = arrayReorder[indexInt];
            }
            return arrayOrdered;
        }
    }

    // Helper class to build and send more complex data objects to views
    public class IndexViewModel
    {
        public IEnumerable<Game> GameCollectionA { get; set; }
        public IEnumerable<Cover> CoverCollectionA { get; set; }
        public IEnumerable<Game> GameCollectionB { get; set; }
        public IEnumerable<Cover> CoverCollectionB { get; set; }
        public IEnumerable<Game> GameCollectionC { get; set; }
        public IEnumerable<Cover> CoverCollectionC { get; set; }
    }
}