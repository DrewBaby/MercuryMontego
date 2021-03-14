using System;
using System.Collections.Generic;

#nullable disable

namespace GameClubProject.Models
{
    public partial class UserGamerTag
    {
        public int PKey { get; set; }
        public string UserId { get; set; }
        public string GamerTag { get; set; }

        public virtual UserAccount User { get; set; }
    }
}
