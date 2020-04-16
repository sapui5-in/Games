using Ludo.UI.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ludo.UI.Class
{
    public class Piece : Button
    {
        private int Position;
        public GameBoardPosition GameBoardPosition;
        public Color Color;

        public Piece(int position, Color color)
        {
            Color = color;
            Position = position;

            this.Location = new System.Drawing.Point(10, 10);
            this.Size = new System.Drawing.Size(30, 30);
            this.UseVisualStyleBackColor = true;
            this.Text = "P";

            if (Color == Enum.Color.Red)
            {
                this.ForeColor = System.Drawing.Color.Red;
            }
            else if (Color == Enum.Color.Green)
            {
                this.ForeColor = System.Drawing.Color.Green;
            }
            else if (Color == Enum.Color.Blue)
            {
                this.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                this.ForeColor = System.Drawing.Color.Yellow;
            }
        }
    }
}
