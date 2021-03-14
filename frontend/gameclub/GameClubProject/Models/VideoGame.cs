using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace GameClubProject.Models
{
    public class VideoGame
    {
        [JsonProperty(PropertyName = "id")]
        [Key]
        public int gameId { get; set; }
        public string name { get; set; }
        public double rating { get; set; }
        public double popularity { get; set; }
        public string summary { get; set; }
        
        public int user_id { get; set; }
        Covers cover { get; set; }
        
        public double first_release_date { get; set; }
        public DateTime release_date { get; set; }

        
    }
}
