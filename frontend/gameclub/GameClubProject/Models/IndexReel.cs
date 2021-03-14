using System.Collections.Generic;

namespace GameClubProject.Models
{
    public class IndexReel
    {
        public IEnumerable<ReelItem> TopRatedGames { get; set; }
        public string GenreATitle { get; set; }
        public string GenreBTitle { get; set; }
        public IEnumerable<ReelItem> GenreAGames { get; set; }
        public IEnumerable<ReelItem> GenreBGames { get; set; }

    }
}
