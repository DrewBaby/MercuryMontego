using System.Collections.Generic;
using Newtonsoft.Json;


namespace GameClubProject.Models
{
    public class PopularGame
    {
        [JsonProperty(PropertyName = "id")]
        public int gameId { get; set; }
        public Covers cover { get; set; }
        public string name { get; set; }
        public double rating { get; set; }
        public double popularity { get; set; }
        public string genre { get; set; }
        public List<Platform> platforms { get; set; }
        public List<Screenshot> screenshots { get; set; }
    }
}
