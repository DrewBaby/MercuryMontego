using System;
using System.Collections.Generic;

#nullable disable

namespace GameClubProject.Models
{
    public partial class IgdbApi
    {
        public int PKey { get; set; }
        public string IgdbClientId { get; set; }
        public string IgdbClientSecret { get; set; }
    }
}
