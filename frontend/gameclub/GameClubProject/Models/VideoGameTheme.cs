using System;
using System.Collections.Generic;

#nullable disable

namespace GameClubProject.Models
{
    public partial class VideoGameTheme
    {
        public int PKey { get; set; }
        public string GameId { get; set; }
        public string ThemeName { get; set; }
        public string ThemeSlug { get; set; }
        public string Url { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual VideoGameMain Game { get; set; }
    }
}
