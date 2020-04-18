using Ludo.UI.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.UI.EventArg
{
    public class PiecePositionChangedEventArgs : EventArgs
    {
        public Piece Piece;
        public GameBoardPosition OldPosition;
        public GameBoardPosition NewPosition;
        public Player Player;
    }
}
