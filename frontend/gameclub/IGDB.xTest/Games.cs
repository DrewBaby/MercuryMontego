using IGDB.Models;
using System.Threading.Tasks;
using Xunit;

namespace IGDB.xTest
{
    public class Games
    {
        IGDBClient _api;


        public Games()
        {

            var IGDB_CLIENT_ID = "suir6rwr94fhs2s3x8jcyfqfjpd6lj";
            var IGDB_CLIENT_SECRET = "a07fcbs0o7est8f2p9o6qpk09t5zfw";

            _api = new IGDB.IGDBClient(IGDB_CLIENT_ID, IGDB_CLIENT_SECRET);
            //  Environment.GetEnvironmentVariable("IGDB_CLIENT_ID"),
            //  Environment.GetEnvironmentVariable("IGDB_CLIENT_SECRET")
            //);

        }

        [Fact]
        public async Task ShouldReturnResponseWithoutQuery()
        {
            var games = await _api.QueryAsync<Game>(IGDBClient.Endpoints.Games);

            Assert.NotNull(games);
            Assert.True(10 == 10);
            Assert.True(games.Count == 10);
            
        }

        [Fact]
        public async Task ShouldReturnResponseWithSingleGame()
        {
            var games = await _api.QueryAsync<Game>(IGDBClient.Endpoints.Games, "fields id,name,genres; where id = 4;");

            Assert.NotNull(games);
            Assert.True(games.Count == 1);

            var game = games[0];

            Assert.Equal("Thief", game.Name);
            Assert.NotEmpty(game.Genres.Ids);
            //Assert.Equal(5, game.Genres.Ids.First());
        }

        [Fact]
        public async Task ShouldReturnResponseWithSingleGameExpandedGenres()
        {
            var games = await _api.QueryAsync<Game>(IGDBClient.Endpoints.Games, "fields id,name,genres.name; where id = 4;");

            Assert.NotNull(games);

            var game = games[0];

            Assert.Equal("Thief", game.Name);
            Assert.NotEmpty(game.Genres.Values);
            Assert.Equal("Shooter", game.Genres.Values[0].Name);
        }

        [Fact]
        public async Task ShouldReturnResponseWithSingleGameCover()
        {
            var games = await _api.QueryAsync<Game>(IGDBClient.Endpoints.Games, "fields id,cover; where id = 4;");

            Assert.NotNull(games);

            var game = games[0];

            Assert.NotNull(game.Cover);
            Assert.NotNull(game.Cover.Id);
            Assert.Equal(96744, game.Cover.Id.Value);
        }

        [Fact]
        public async Task ShouldReturnResponseWithSingleGameExpandedCover()
        {
            var games = await _api.QueryAsync<Game>(IGDBClient.Endpoints.Games, "fields id,cover.*; where id = 4;");

            Assert.NotNull(games);

            var game = games[0];

            Assert.NotNull(game.Cover);
            Assert.NotNull(game.Cover.Value);
            Assert.Equal(756, game.Cover.Value.Width);
        }

        [Fact]
        public async Task ShouldReturnResponseWithUnixTimestamp()
        {
            var games = await _api.QueryAsync<Game>(IGDBClient.Endpoints.Games, "fields id,created_at; where id = 4;");

            Assert.NotNull(games);

            var game = games[0];

            Assert.NotNull(game.Created_At);
            Assert.True(game.Created_At.Value.Year > 1970);
        }
    }
}
