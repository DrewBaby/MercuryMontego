using System;
using System.Collections.Generic;

#nullable disable

namespace GameClubProject
{
    public partial class UserAccount
    {
        public UserAccount()
        {
            PersonalUserTrackedGames = new HashSet<PersonalUserTrackedGame>();
            UserGamerTags = new HashSet<UserGamerTag>();
            VideoGameUserContents = new HashSet<VideoGameUserContent>();
        }

        public int PKey { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int MembershipStatusId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AddressStreet { get; set; }
        public string AddressStreet2 { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string AddressZipCode { get; set; }
        public byte[] CreatedAt { get; set; }

        public virtual MembershipStatus MembershipStatus { get; set; }
        public virtual ICollection<PersonalUserTrackedGame> PersonalUserTrackedGames { get; set; }
        public virtual ICollection<UserGamerTag> UserGamerTags { get; set; }
        public virtual ICollection<VideoGameUserContent> VideoGameUserContents { get; set; }
    }
}
