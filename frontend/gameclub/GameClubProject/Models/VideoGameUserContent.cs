using System;
using System.Collections.Generic;

#nullable disable

namespace GameClubProject
{
    public partial class VideoGameUserContent
    {
        public int PKey { get; set; }
        public string UserId { get; set; }
        public string GameId { get; set; }
        public decimal UserRating { get; set; }
        public string UserReview { get; set; }

        public virtual VideoGameMain Game { get; set; }
        public virtual UserAccount User { get; set; }
    }
}
