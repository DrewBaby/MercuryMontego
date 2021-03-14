using System;
using System.Collections.Generic;

#nullable disable

namespace GameClubProject.Models
{
    public partial class VideoGameSimilarTitle
    {
        public int PKey { get; set; }
        public string HostGameId { get; set; }
        public string SimilarGameId { get; set; }

        public virtual VideoGameMain HostGame { get; set; }
    }
}
