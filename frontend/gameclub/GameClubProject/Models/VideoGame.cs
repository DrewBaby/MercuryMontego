using System;
using IGDB.Models;
using IGDB;
using System.ComponentModel.DataAnnotations;

namespace GameClubProject.Models
{
    public class VideoGame
    {
        // Various game properties
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Category { get; set; }
        public int GameStatus { get; set; }
        public int AgeRatingCategory { get; set; }
        public int AgeRatingTitle { get; set; }
        public int AgeRatingSynopsis { get; set; }
        public double Rating { get; set; }
        public int TotalReviews { get; set; }
        [DataType(DataType.Date)]
        public DateTime FirstReleaseDate { get; set; }
        public string VersionTitle { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.Date)]
        public DateTime UpdatedAt { get; set; }

        // Multiplayer specific properties
        public bool CampaignCoop { get; set; }
        public bool DropIn { get; set; }
        public bool LanCoop { get; set; }
        public bool OfflineCoop { get; set; }
        public int OfflineCoopMax { get; set; }
        public bool OnlineCoop { get; set; }
        public int OnlineCoopMax { get; set; }
        public int OnlineMax { get; set; }
        public bool Splitscreen { get; set; }
        public bool SplitscreenOnline { get; set; }

        // Properties specific to lists
        // These will most likely be stored in seperate SQL tables
        public string[] AlternativeTitle { get; set; }
        public string[] GameMode { get; set; }
        public Character[] Character { get; set; }
        public Company[] Company { get; set; }
        public string[] Platform { get; set; }
        public string[] Genre { get; set; }
        public string[] ImageUrl { get; set; }

    }

    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Species { get; set; }
        public string CountryName { get; set; }
        public string Description { get; set; }
        public int Gender { get; set; }
        public string[] Alias { get; set; }
        public string[] ImageUrl { get; set; }
    }

    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ParentCompany { get; set; }
        public DateTime StartDate { get; set; }
        public string Url { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
    }
}
