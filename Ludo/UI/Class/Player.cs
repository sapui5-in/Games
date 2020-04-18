using Ludo.UI.Enum;
using Ludo.UI.EventArg;
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
        public delegate void EventHandler(object sender, PiecePositionChangedEventArgs e);
        public delegate void PieceClickEventHandler(object sender, PieceClickEventArgs e);

        public event EventHandler PieceMoved;
        public event PieceClickEventHandler PieceClicked;

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
                this.SetInitialPiecePosition();
            }
        }

        public Player(Enum.Color color)
        {
            Color = color;
            for (int i = 0; i < Pieces.Length; i++)
            {
                Pieces[i] = new Piece(i, Color);
                Pieces[i].PositionChanged += this.Piece_PositionChanged;
                Pieces[i].Click += this.Piece_Clicked;
            }
        }

        public void DisableAllPieceMovement()
        {
            foreach (Piece piece in Pieces)
            {
                piece.Movable = false;
            }
        }

        private void Piece_Clicked(object sender, EventArgs e)
        {
            foreach (Piece piece in Pieces)
            {
                piece.Movable = false;
            }

            if (PieceClicked != null)
            {
                PieceClicked(this, new PieceClickEventArgs
                {
                    Piece = (sender as Piece)
                });
            }
        }

        private void Piece_PositionChanged(object sender, PiecePositionChangedEventArgs e)
        {
            if (PieceMoved != null)
            {
                PieceMoved(this, new PiecePositionChangedEventArgs
                {
                    Piece = (sender as Piece),
                    OldPosition = e.OldPosition,
                    NewPosition = e.NewPosition,
                    Player = this
                });
            }
        }

        public Player(System.Drawing.Color color)
        {
            this.red = color;
        }

        private void SetInitialPiecePosition()
        {
            for (int i = 0; i < this.Pieces.Length; i++)
            {
                this.Pieces[i].GameBoardPosition = new GameBoardPosition(Quadrant, Quadrant.QuadrantHome.GhorPositions[i]);
            }
        }
    }
}