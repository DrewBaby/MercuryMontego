using System;
using System.Collections.Generic;

#nullable disable

namespace GameClubProject
{
    public partial class VideoGameMain
    {
        public VideoGameMain()
        {
            AlternativeGameNames = new HashSet<AlternativeGameName>();
            PersonalUserTrackedGames = new HashSet<PersonalUserTrackedGame>();
            VideoGameCharacters = new HashSet<VideoGameCharacter>();
            VideoGameGameModes = new HashSet<VideoGameGameMode>();
            VideoGameGenres = new HashSet<VideoGameGenre>();
            VideoGameInvolvedCompanies = new HashSet<VideoGameInvolvedCompany>();
            VideoGameKeywords = new HashSet<VideoGameKeyword>();
            VideoGameMultiplayerModes = new HashSet<VideoGameMultiplayerMode>();
            VideoGamePlatforms = new HashSet<VideoGamePlatform>();
            VideoGameThemes = new HashSet<VideoGameTheme>();
            VideoGameUserContents = new HashSet<VideoGameUserContent>();
        }

        public int PKey { get; set; }
        public string GameId { get; set; }
        public string GameTitle { get; set; }
        public string Description { get; set; }
        public int? Category { get; set; }
        public int? GameStatus { get; set; }
        public int? AgeRatingCategory { get; set; }
        public int? AgeRatingTitle { get; set; }
        public string AgeRatingSynopsis { get; set; }
        public decimal? Rating { get; set; }
        public int? TotalReviews { get; set; }
        public DateTime? FirstReleaseDate { get; set; }
        public string VersionTitle { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<AlternativeGameName> AlternativeGameNames { get; set; }
        public virtual ICollection<PersonalUserTrackedGame> PersonalUserTrackedGames { get; set; }
        public virtual ICollection<VideoGameCharacter> VideoGameCharacters { get; set; }
        public virtual ICollection<VideoGameGameMode> VideoGameGameModes { get; set; }
        public virtual ICollection<VideoGameGenre> VideoGameGenres { get; set; }
        public virtual ICollection<VideoGameInvolvedCompany> VideoGameInvolvedCompanies { get; set; }
        public virtual ICollection<VideoGameKeyword> VideoGameKeywords { get; set; }
        public virtual ICollection<VideoGameMultiplayerMode> VideoGameMultiplayerModes { get; set; }
        public virtual ICollection<VideoGamePlatform> VideoGamePlatforms { get; set; }
        public virtual ICollection<VideoGameTheme> VideoGameThemes { get; set; }
        public virtual ICollection<VideoGameUserContent> VideoGameUserContents { get; set; }
    }
}
