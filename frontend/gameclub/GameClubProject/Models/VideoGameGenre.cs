using System;
using System.Collections.Generic;

#nullable disable

namespace GameClubProject.Models
{
    public partial class VideoGameGenre
    {
        public int PKey { get; set; }
        public string GameId { get; set; }
        public string GenreName { get; set; }
        public string Slug { get; set; }
        public string Url { get; set; }
        public string Checksum { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual VideoGameMain Game { get; set; }
    }
}
