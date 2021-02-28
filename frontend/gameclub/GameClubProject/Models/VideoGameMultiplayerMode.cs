using System;
using System.Collections.Generic;

#nullable disable

namespace GameClubProject
{
    public partial class VideoGameMultiplayerMode
    {
        public int PKey { get; set; }
        public string GameId { get; set; }
        public bool? CampaignCoop { get; set; }
        public bool? DropIn { get; set; }
        public bool? LanCoop { get; set; }
        public bool? OfflineCoop { get; set; }
        public int? OfflineCoopMax { get; set; }
        public bool? OnlineCoop { get; set; }
        public int? OnlineCoopMax { get; set; }
        public int? OnlineMax { get; set; }
        public bool? Splitscreen { get; set; }
        public bool? SplitscreenOnline { get; set; }

        public virtual VideoGameMain Game { get; set; }
    }
}
