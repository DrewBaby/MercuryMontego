﻿using System;
using System.Collections.Generic;

#nullable disable

namespace GameClubProject
{
    public partial class AlternativeGameName
    {
        public int PKey { get; set; }
        public string GameId { get; set; }
        public string GameTitle { get; set; }
        public string AltName { get; set; }
        public string CheckSum { get; set; }

        public virtual VideoGameMain Game { get; set; }
    }
}