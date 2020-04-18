using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ludo.UI.Class
{
    public class DiceRenderer : Button
    {
        public DiceRenderer()
            : base()
        {
            this.Size = new System.Drawing.Size(60, 60);
            this.ForeColor = System.Drawing.Color.White;
            this.Text = "0";
        }
    }
}
