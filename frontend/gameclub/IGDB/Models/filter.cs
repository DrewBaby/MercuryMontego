using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace IGDB.Models
{
    public class PopularGame
    {
        [JsonProperty(PropertyName = "id")]
        public int gameId { get; set; }
        public Cover cover { get; set; }
        public string name { get; set; }
        public double rating { get; set; }
        public double popularity { get; set; }
        public List<Platform> platforms { get; set; }
        public List<Screenshot> screenshots { get; set; }
    }

    public class Filters
    {
        public List<Genre> genres { get; set; }
        public List<string> platforms = new List<string>(){
            "Linux","Mac","PC(Microsoft Windows)","Nintendo","Playstation 4","PlayStation 3","Xbox One","Xbox 360"
        };

    }
}
