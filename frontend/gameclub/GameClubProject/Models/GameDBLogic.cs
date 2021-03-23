using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using IGDB.Models;
using IGDB;


namespace GameClubProject.Models
{
    public class GameDBLogic
    {
        static protected string IGDB_CLIENT_ID = "suir6rwr94fhs2s3x8jcyfqfjpd6lj";
        static protected string IGDB_CLIENT_SECRET = "a07fcbs0o7est8f2p9o6qpk09t5zfw";

        public static async Task<List<Game>> LoadGamesBasedOnFilters(string genre, string platform, int MinRating = 1, int MaxRating = 100)
        {
            var _api = new IGDB.IGDBClient(IGDB_CLIENT_ID, IGDB_CLIENT_SECRET);
            var query = queryString(MinRating, MaxRating, genre: genre, platform: platform);
            var games = await _api.QueryAsync<Game>(IGDBClient.Endpoints.Games, query);
            try
            {
                System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                foreach (var game in games)
                {
                    //Added the ability to convert from decimal to datetime format. 
                    game.FirstReleaseDate = dateTime.AddSeconds(Convert.ToSingle(game.FirstReleaseDate));
                }
                return games;
            }
            catch (Exception)
            {
                var emptyGameList = new List<Game>();
                return emptyGameList;
            }
        }

        public static async Task<List<PopularGame>> LoadPopularGames()
        {
            var _api = new IGDB.IGDBClient(IGDB_CLIENT_ID, IGDB_CLIENT_SECRET);
            var query = (@"
            fields screenshots.url,cover.url,cover.image_id,name,rating,first_release_date,platforms.name;
            where first_release_date > 1527206400 & first_release_date < 1546254191;
            where rating>1;sort rating desc; 
            limit 6;");

            try
            {
                var PopularGames = await _api.QueryAsync<PopularGame>(IGDBClient.Endpoints.Games, query);
                //var PopularGames = await response.Content.ReadAsAsync<List<PopularGame>>();
                return PopularGames;
            }
            catch
            {
                var emptyGameList = new List<PopularGame>();
                return emptyGameList;
                // throw new Exception(response.ReasonPhrase);
            }
        }

        public static async Task<List<PopularGame>> LoadGenreAGames()
        {
            var _api = new IGDB.IGDBClient(IGDB_CLIENT_ID, IGDB_CLIENT_SECRET);
            var query = (@"
            fields screenshots.url,cover.url,cover.image_id,cover.url,name,rating,first_release_date,platforms.name,genres.name;
            where cover != null & rating != null & rating > 90 & genres = (12); limit 10;");
            
            try
            {
                var genreAGames = await _api.QueryAsync<PopularGame>(IGDBClient.Endpoints.Games, query);
                return genreAGames;
            }
            catch
            {
                var emptyGameList = new List<PopularGame>();
                return emptyGameList;
            }
        }
        public static async Task<List<PopularGame>> LoadGenreBGames()
        {
            var _api = new IGDB.IGDBClient(IGDB_CLIENT_ID, IGDB_CLIENT_SECRET);
            var query = (@"
            fields screenshots.url,cover.url,cover.image_id,cover.url,name,rating,first_release_date,platforms.name,genres.name;
            where cover != null & rating != null & rating > 90 & genres = (5); limit 10;");

            try
            {
                var genreBGames = await _api.QueryAsync<PopularGame>(IGDBClient.Endpoints.Games, query);
                return genreBGames;
            }
            catch
            {
                var emptyGameList = new List<PopularGame>();
                return emptyGameList;
            }
        }
        public static async Task<List<PopularGame>> LoadGenreCGames()
        {
            var _api = new IGDB.IGDBClient(IGDB_CLIENT_ID, IGDB_CLIENT_SECRET);
            var query = (@"
            fields screenshots.url,cover.url,cover.image_id,cover.url,name,rating,first_release_date,platforms.name,genres.name;
            where cover != null & rating != null & rating > 90 & genres = (31); limit 10;");

            try
            {
                var genreBGames = await _api.QueryAsync<PopularGame>(IGDBClient.Endpoints.Games, query);
                return genreBGames;
            }
            catch
            {
                var emptyGameList = new List<PopularGame>();
                return emptyGameList;
            }
        }

        public static async Task<Filters> FetchGenres()
        {
            var _api = new IGDB.IGDBClient(IGDB_CLIENT_ID, IGDB_CLIENT_SECRET);            
            var query = (@"fields name,slug; limit 30; ");
            try
            {
                var genres = await _api.QueryAsync<Genre>(IGDBClient.Endpoints.Genres, query);
                var filters = new Filters();
                filters.genres = genres;
                return filters;
            }
            catch
            {
                throw new Exception("error");
            }
        }
        public static async Task<List<Game>> LoadGamesBasedOnSearch(string name)
        {
            var _api = new IGDB.IGDBClient(IGDB_CLIENT_ID, IGDB_CLIENT_SECRET);
            var query = $@"
                fields genres.name,genres.slug,cover.url,cover.image_id,name,rating,first_release_date,platforms.name;
                where rating>1;
                search ""{name}"";
                limit 20;";
            try
            {
                var games = await _api.QueryAsync<Game>(IGDBClient.Endpoints.Games, query);
                System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                foreach (var game in games)
                {
                    dateTime = dateTime.AddSeconds(game.Freshfirst_release_date);
                    game.release_date = dateTime;
                }
                return games;
            }
            catch
            {
                throw new Exception("error");
            }
        }
        public static async Task<Game> ShowOneGame(int gameId)
        {
            var _api = new IGDB.IGDBClient(IGDB_CLIENT_ID, IGDB_CLIENT_SECRET);
            var query = $@"
                fields genres.name,cover.image_id,cover.url,name,
                rating,first_release_date,platforms.name,artworks,artworks.url,
                storyline,summary,screenshots.image_id,screenshots.url,videos.video_id,videos.name,
                expansions.name,expansions.cover.image_id,involved_companies.company.name;
                where id={gameId};";

            try
            {
                var games = await _api.QueryAsync<Game>(IGDBClient.Endpoints.Games, query);
                System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                foreach (var game in games)
                {
                    dateTime = dateTime.AddSeconds(game.Freshfirst_release_date);
                    game.release_date = dateTime;

                    if (game.Cover != null)
                    {
                        game.Id = gameId;
                    }

                    if (game.Screenshots != null)
                    {
                        foreach (var screenshot in game.Screenshots.Values)
                        {
                            screenshot.gameid = gameId;
                        }
                    }

                    if (game.Videos != null)
                    {
                        foreach (var videos in game.Videos.Values)
                        {
                            videos.gameid = gameId;
                        }
                    }

                }
                return games[0];
            }
            catch
            {
                throw new Exception("Error");
            }

        }
        public static string queryString(int MinRating, int MaxRating, string genre, string platform)
        {
            string query;
            if (genre == null && platform == null)
            {
                query = $@"
                fields genres.name,genres.slug,cover.url,cover.image_id,name,rating,first_release_date,platforms.name;
                where rating>{MinRating}&rating<{MaxRating};
                sort rating desc;
                limit 20;";
            }
            else if (genre == null && platform != null)
            {
                query = $@"
                    fields genres.name,genres.slug,cover.url,cover.image_id,name,rating,first_release_date,platforms.name;
                    where rating>{MinRating}&rating<{MaxRating};
                    where platforms.name~*""{platform}""*;
                    sort rating desc;
                    limit 20;";
            }
            else if (genre != null && platform == null)
            {
                query = $@"
                    fields genres.name,genres.slug,cover.url,cover.image_id,name,rating,first_release_date,platforms.name;
                    where rating>{MinRating}&rating<{MaxRating};
                    where genres.slug~*""{genre}""*;
                    where genres.slug~*""{genre}""*;
                    sort rating desc;
                    limit 20;";
            }
            else
            {
                query = $@"
                    fields genres.name,genres.slug,cover.url,cover.image_id,name,rating,first_release_date,platforms.name;
                    where rating>{MinRating}&rating<{MaxRating};
                    where genres.slug~*""{genre}"";
                    where genres.slug~*""{genre}""*;
                    where platforms.name~*""{platform}""*;
                    sort rating desc;
                    limit 20;";
            }
            return query;
        }

    }
}

