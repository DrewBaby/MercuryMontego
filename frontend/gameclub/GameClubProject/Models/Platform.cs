using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace GameClubProject.Models
{
    public class Platform : VideoGamePlatform
    {
        [JsonProperty(PropertyName = "id")]
        [Key]
        public int platform_id { get; set; }
        public string name { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; } = DateTime.Now;        
        public List<GamePlatforms> HostingGames { get; set; }
    }
}
