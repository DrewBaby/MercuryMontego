using System;
using System.Collections.Generic;

#nullable disable

namespace GameClubProject.Models
{
    public partial class VideoGameMain
    {
        public VideoGameMain()
        {
            PersonalUserTrackedGames = new HashSet<PersonalUserTrackedGame>();
            VideoGameUserContents = new HashSet<VideoGameUserContent>();
        }

        public int PKey { get; set; }
        public string GameId { get; set; }

        public virtual ICollection<PersonalUserTrackedGame> PersonalUserTrackedGames { get; set; }
        public virtual ICollection<VideoGameUserContent> VideoGameUserContents { get; set; }
    }
}
