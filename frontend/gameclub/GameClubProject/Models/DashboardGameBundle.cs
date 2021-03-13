using System;
using System.Collections.Generic;
using IGDB.Models;



namespace GameClubProject.Models
{
    public class DashboardGameBundle
    {
        public List<Game> games { get; set; }
        public List<PopularGame> popularGames { get; set; }
        public String user { get; set; }
        public RatingForm rating_form { get; set; }
    }
}
