using System;
using System.Collections.Generic;

#nullable disable

namespace GameClubProject.Models
{
    public partial class VideoGameScreenshot
    {
        public int PKey { get; set; }
        public string GameId { get; set; }
        public string ImageId { get; set; }
        public bool? AlphaChannel { get; set; }
        public bool? Animated { get; set; }
        public string Url { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }

        public virtual VideoGameMain Game { get; set; }
    }
}
