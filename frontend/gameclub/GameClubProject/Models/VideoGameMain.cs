using System;
using System.Collections.Generic;

#nullable disable

namespace GameClubProject.Models
{
    public partial class VideoGameMain
    {
        public VideoGameMain()
        {
            PersonalUserTrackedGames = new HashSet<PersonalUserTrackedGame>();
            VideoGameAlternativeNames = new HashSet<VideoGameAlternativeName>();
            VideoGameArtworks = new HashSet<VideoGameArtwork>();
            VideoGameGameModes = new HashSet<VideoGameGameMode>();
            VideoGameGenres = new HashSet<VideoGameGenre>();
            VideoGameInvolvedCompanies = new HashSet<VideoGameInvolvedCompany>();
            VideoGameKeywords = new HashSet<VideoGameKeyword>();
            VideoGameMultiplayerModes = new HashSet<VideoGameMultiplayerMode>();
            VideoGamePlatforms = new HashSet<VideoGamePlatform>();
            VideoGameScreenshots = new HashSet<VideoGameScreenshot>();
            VideoGameSimilarTitles = new HashSet<VideoGameSimilarTitle>();
            VideoGameThemes = new HashSet<VideoGameTheme>();
            VideoGameUserContents = new HashSet<VideoGameUserContent>();
        }

        public int PKey { get; set; }
        public string GameId { get; set; }
        public string GameTitle { get; set; }
        public string Summary { get; set; }
        public string Storyline { get; set; }
        public int? Category { get; set; }
        public string ImageId { get; set; }
        public bool? AlphaChannel { get; set; }
        public bool? Animated { get; set; }
        public string CoverUrl { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
        public string GameStatus { get; set; }
        public decimal? AggregatedRating { get; set; }
        public int? AggregatedRatingCount { get; set; }
        public DateTime? FirstReleaseDate { get; set; }
        public string VersionTitle { get; set; }
        public string Url { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual VideoGameCharacter VideoGameCharacter { get; set; }
        public virtual ICollection<PersonalUserTrackedGame> PersonalUserTrackedGames { get; set; }
        public virtual ICollection<VideoGameAlternativeName> VideoGameAlternativeNames { get; set; }
        public virtual ICollection<VideoGameArtwork> VideoGameArtworks { get; set; }
        public virtual ICollection<VideoGameGameMode> VideoGameGameModes { get; set; }
        public virtual ICollection<VideoGameGenre> VideoGameGenres { get; set; }
        public virtual ICollection<VideoGameInvolvedCompany> VideoGameInvolvedCompanies { get; set; }
        public virtual ICollection<VideoGameKeyword> VideoGameKeywords { get; set; }
        public virtual ICollection<VideoGameMultiplayerMode> VideoGameMultiplayerModes { get; set; }
        public virtual ICollection<VideoGamePlatform> VideoGamePlatforms { get; set; }
        public virtual ICollection<VideoGameScreenshot> VideoGameScreenshots { get; set; }
        public virtual ICollection<VideoGameSimilarTitle> VideoGameSimilarTitles { get; set; }
        public virtual ICollection<VideoGameTheme> VideoGameThemes { get; set; }
        public virtual ICollection<VideoGameUserContent> VideoGameUserContents { get; set; }
    }
}
