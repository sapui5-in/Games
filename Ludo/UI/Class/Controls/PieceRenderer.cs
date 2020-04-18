using Ludo.UI.Enum;
using Ludo.UI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ludo.UI.Class
{
    public class PieceRenderer : Button
    {
        public PieceRenderer()
            : base()
        {
            this.Location = new System.Drawing.Point(10, 10);
            this.Size = new System.Drawing.Size(30, 30);
            this.UseVisualStyleBackColor = true;
            this.Text = "P";
            this.Enabled = false;
        }

        public void SetColor(Color color)
        {
            this.ForeColor = Util.GetDrawingColor(color);
        }
    }
}
