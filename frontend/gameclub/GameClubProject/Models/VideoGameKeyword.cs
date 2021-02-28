using System;
using System.Collections.Generic;

#nullable disable

namespace GameClubProject
{
    public partial class VideoGameKeyword
    {
        public int PKey { get; set; }
        public string GameId { get; set; }
        public string Keyword { get; set; }

        public virtual VideoGameMain Game { get; set; }
    }
}
