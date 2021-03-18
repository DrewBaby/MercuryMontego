using System;
using System.Collections.Generic;

#nullable disable

namespace GameClubProject.Models
{
    public partial class VideoGameAlternativeName
    {
        public int PKey { get; set; }
        public string GameId { get; set; }
        public string GameTitle { get; set; }
        public string AltName { get; set; }
        public string CheckSum { get; set; }

        public virtual VideoGameMain Game { get; set; }
    }
}
