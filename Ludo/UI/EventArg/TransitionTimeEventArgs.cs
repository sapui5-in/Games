using Ludo.UI.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.UI.EventArg
{
    public class TransitionTimeEventArgs : EventArgs
    {
        public Piece Piece;
        public GameBoardPosition Position;
        public List<GameBoardPosition> TransitionPositions;
    }
}
