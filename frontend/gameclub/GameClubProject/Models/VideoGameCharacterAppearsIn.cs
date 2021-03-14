using System;
using System.Collections.Generic;

#nullable disable

namespace GameClubProject.Models
{
    public partial class VideoGameCharacterAppearsIn
    {
        public int PKey { get; set; }
        public string CharacterId { get; set; }
        public string GameId { get; set; }

        public virtual VideoGameCharacter Character { get; set; }
    }
}
