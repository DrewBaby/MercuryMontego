using System;
using System.Collections.Generic;

#nullable disable

namespace GameClubProject.Models
{
    public partial class PersonalUserTrackedGame
    {
        public int PKey { get; set; }
        public string UserId { get; set; }
        public string GameId { get; set; }

        public virtual VideoGameMain Game { get; set; }
        public virtual UserAccount User { get; set; }
    }
}
