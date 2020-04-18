using Ludo.UI.Enum;
using Ludo.UI.EventArg;
using Ludo.UI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ludo.UI.Class
{
    public class Piece
    {
        public PieceRenderer PieceRenderer = new PieceRenderer();

        public delegate void EventHandler(object sender, PiecePositionChangedEventArgs e);
        public event EventHandler PositionChanged;

        public delegate void ClickEventHandler(object sender, EventArgs e);
        public event ClickEventHandler Click;

        public int Position;
        public Color Color;

        private GameBoardPosition gameBoardPosition;
        public GameBoardPosition GameBoardPosition
        {
            get
            {
                return gameBoardPosition;
            }
            set
            {
                PiecePositionChangedEventArgs EvtArgs = new PiecePositionChangedEventArgs
                {
                    Piece = this,
                    OldPosition = gameBoardPosition,
                    NewPosition = value
                };

                gameBoardPosition = value;

                if (PositionChanged != null && value != null)
                {
                    PositionChanged(this, EvtArgs);
                }
            }
        }

        private bool movable = false;
        public bool Movable
        {
            get
            {
                return movable;
            }
            set
            {
                movable = value;
                if (Movable)
                {
                    this.PieceRenderer.Enabled = true;
                }
                else
                {
                    this.PieceRenderer.Enabled = false;
                }
            }
        }

        public Piece(int position, Color color)
            : base()
        {
            Color = color;
            Position = position;

            PieceRenderer.SetColor(color);

            PieceRenderer.Click += this.PieceClick;
        }

        public void PieceClick(object sender, EventArgs e)
        {
            if (Click != null)
            {
                Click(this, e);
            }
        }
    }
}
