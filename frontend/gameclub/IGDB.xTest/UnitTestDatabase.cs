using System;
using Xunit;
using GameClubProject.Models;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace IGDB.xTest
{
    // Helper class for testing the selection of data from the database
    public class VideoGameMainQuery
    {
        private GameclubDBContext _context;

        public VideoGameMainQuery(GameclubDBContext context)
        {
            _context = context;
        }

        public IEnumerable<VideoGameMain> SelectSomeData()
        {
            throw new NotImplementedException();

            //return _context.VideoGameMains.Find(1)
        }
    }

    public class IntegrationTest
    {
        GameclubDBContext _context;

        public IntegrationTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();
            var builder = new DbContextOptionsBuilder<GameclubDBContext>();

            builder.UseSqlServer($"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GameclubDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                .UseInternalServiceProvider(serviceProvider);
            _context = new GameclubDBContext(builder.Options);
            //_context.Database.EnsureCreated();
        }

        [Fact]
        public void DatabaseInsert_WhenUserTrackGame()
        {
            //Use Add() to insert a sample row of data
            _context.VideoGameMains.Add(new VideoGameMain
            {
                GameId = "10454",
                GameTitle = "Final Fantasy VII Remake",
                Summary = "It's a game.",
                Storyline = "Similar to the original.",
                Category = 4,
                ImageId = "543213",
                AlphaChannel = false,
                Animated = false,
                CoverUrl = "http://google.com/",
                Height = 200,
                Width = 100,
                GameStatus = "MainGame",
                AggregatedRating = 9,
                AggregatedRatingCount = 142,
                FirstReleaseDate = new DateTime(2020, 3, 15),
                VersionTitle = "Mako Edition",
                Url = "http://bellevuecollege.edu/",
                CreatedAt = null,
                UpdatedAt = null
            });
            _context.VideoGameCharacters.Add(new VideoGameCharacter
            {
                GameId = "10454",
                CharacterId = "1243206785",
                Name = "Barret Wallus",
                Species = 2,
                CountryName = "Midgar",
                Description = "Man with gun for arm.",
                Gender = 1,
                MugshotId = 76584536,
                AlphaChannel = false,
                Animated = false,
                Url = "http://square.com/",
                Height = 200,
                Width = 100
            });
            _context.SaveChanges();
        }
    }
}