using System;
using System.Collections.Generic;

#nullable disable

namespace GameClubProject
{
    public partial class VideoGameCharacterAlias
    {
        public int Pkey { get; set; }
        public string CharacterId { get; set; }
        public string AliasName { get; set; }

        public virtual VideoGameCharacter Character { get; set; }
    }
}
