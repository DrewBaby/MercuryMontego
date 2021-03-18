using System;
using System.Collections.Generic;

#nullable disable

namespace GameClubProject.Models
{
    public partial class VideoGameInvolvedCompany
    {
        public int PKey { get; set; }
        public string GameId { get; set; }
        public string CompanyName { get; set; }
        public string ParentCompany { get; set; }
        public DateTime? StartDate { get; set; }
        public string Url { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }

        public virtual VideoGameMain Game { get; set; }
    }
}
