using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace GameClubProject.Models
{
    public class Covers
    {
        [JsonProperty(PropertyName = "id")]
        [Key]
        public int cover_id { get; set; }
        public int gameId { get; set; }
        public string image_id { get; set; }
        public string url { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; } = DateTime.Now;

    }
}
