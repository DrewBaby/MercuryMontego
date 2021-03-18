using System;
using System.Collections.Generic;

#nullable disable

namespace GameClubProject.Models
{
    public partial class VideoGameGameMode
    {
        public int PKey { get; set; }
        public string GameId { get; set; }
        public string GameModeName { get; set; }
        public string GameModeUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Slug { get; set; }

        public virtual VideoGameMain Game { get; set; }
    }
}
