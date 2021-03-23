using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GameClubProject.Models
{
    public class GamePlatforms 
    {
        [Key]        
        public int gamePlatform_id { get; set; }
        public int gameId { get; set; }
        public int platform_id { get; set; }
        public IGDB.Models.Game game { get; set; }
        public Platform platform { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; } = DateTime.Now;

    }
}
