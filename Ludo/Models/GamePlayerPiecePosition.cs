﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.API.Model
{
    public class GamePlayerPiecePosition
    {
        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        public int PlayerId { get; set; }

        public virtual User Player { get; set; }

        public int PieceNumber { get; set; }

        public string GhorType { get; set; }

        public int GhorPosition { get; set; }

        public int Quadrant { get; set; }

    }
}
