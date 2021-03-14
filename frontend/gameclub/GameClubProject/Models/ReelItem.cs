using IGDB.Models;

namespace GameClubProject.Models
{
    public class ReelItem
    {
        public Game Game { get; set; }
        public Cover Cover { get; set; }

        public ReelItem(Game game, Cover cover)
        {
            Game = game;
            Cover = cover;
        }
    }
}
