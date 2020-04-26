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
        public List<GameBoardPosition> Positions;

        public PiecePosition(Piece piece, List<GameBoardPosition> positions)
        {
            Piece = piece;
            Positions = positions;
        }
    }
}