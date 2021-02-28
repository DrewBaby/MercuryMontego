using System;
using System.Collections.Generic;

#nullable disable

namespace GameClubProject
{
    public partial class MembershipStatus
    {
        public MembershipStatus()
        {
            UserAccounts = new HashSet<UserAccount>();
        }

        public int PKey { get; set; }
        public int MembershipStatusId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }
}
