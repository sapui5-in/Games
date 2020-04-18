using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.UI.Class
{
    public class PiecePosition
    {
        public Piece Piece;
        public GameBoardPosition Position;

        public PiecePosition(Piece piece, GameBoardPosition position)
        {
            Piece = piece;
            Position = position;
        }
    }
}