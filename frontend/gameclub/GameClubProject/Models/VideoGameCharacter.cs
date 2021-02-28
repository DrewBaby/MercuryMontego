using System;
using System.Collections.Generic;

#nullable disable

namespace GameClubProject
{
    public partial class VideoGameCharacter
    {
        public VideoGameCharacter()
        {
            VideoGameCharacterAliases = new HashSet<VideoGameCharacterAlias>();
        }

        public int PKey { get; set; }
        public string GameId { get; set; }
        public string CharacterId { get; set; }
        public string Name { get; set; }
        public int? Species { get; set; }
        public string CountryName { get; set; }
        public string Description { get; set; }
        public int? Gender { get; set; }

        public virtual VideoGameMain Game { get; set; }
        public virtual ICollection<VideoGameCharacterAlias> VideoGameCharacterAliases { get; set; }
    }
}
