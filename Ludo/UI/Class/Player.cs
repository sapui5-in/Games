using Ludo.UI.Enum;
using Ludo.UI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.UI.Class
{
    public class Player : IPlayer
    {
        public Piece[] Pieces = new Piece[4];

        public Enum.Color Color;

        public Game Game;

        private Quadrant quadrant;
        private System.Drawing.Color red;

        public Quadrant Quadrant
        {
            get
            {
                return quadrant;
            }
            set
            {
                quadrant = value;
                //this.Color = Quadrant.Color;
                this.SetInitialPiecePosition();
            }
        }

        public Player(Enum.Color color)
        {
            Color = color;
            for (int i = 0; i < Pieces.Length; i++)
            {
                Pieces[i] = new Piece(i, Color);
            }
        }

        public Player(System.Drawing.Color red)
        {
            this.red = red;
        }

        private void SetInitialPiecePosition()
        {
            for (int i = 0; i < this.Pieces.Length; i++)
            {
                this.Pieces[i].GameBoardPosition = new GameBoardPosition(Quadrant, Quadrant.QuadrantHome.GhorPositions[i]);
            }
        }

        // Check Move Feasibility

        // Move Piece - Final Destination
        public void MovePiece(Piece piece, int count)
        {
            // Show Transition
            GameBoardPosition newPosition = this.CheckValidMove(piece, count);

            if (newPosition != null)
            {
                // Show Transition

                //Move to Final Position
                piece.GameBoardPosition = newPosition;
            }
        }

        public GameBoardPosition CheckValidMove(Piece piece, int count)
        {
            bool flag = true;

            if (piece.GameBoardPosition.Ghor.Position != -1)
            {
                // From Home, only 6 can make him move to Start Star
                if (piece.GameBoardPosition.Ghor.GhorType == GhorType.Home)
                {
                    if (count == 6)
                    {
                        count = 1;
                    }
                    else
                    {
                        flag = false;
                    }
                }

                if (flag)
                {
                    GameBoardPosition currentPiecePosition = new GameBoardPosition(piece.GameBoardPosition.Quadrant, piece.GameBoardPosition.Ghor);
                    return Game.GameBoard.GetNthGhorPosition(currentPiecePosition, count, this);
                }
            }

            return null;
        }
    }
}