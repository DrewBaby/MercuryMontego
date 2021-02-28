using System;
using System.Collections.Generic;

#nullable disable

namespace GameClubProject
{
    public partial class VideoGamePlatform
    {
        public int PKey { get; set; }
        public string GameId { get; set; }
        public string Platform { get; set; }

        public virtual VideoGameMain Game { get; set; }
    }
}
