using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace GameClubProject.Models
{
    public class Screenshot
    {
        [JsonProperty(PropertyName = "id")]
        [Key]
        public int screenshot_id { get; set; }
        public int gameId { get; set; }
        public string image_id { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; } = DateTime.Now;

    }
}
