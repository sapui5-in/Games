using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ludo.UI.Class.Controls
{
    public class GhorRenderer : Panel
    {
        public GhorRenderer()
            : base()
        {
            this.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left) | AnchorStyles.Right)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Size = new System.Drawing.Size(49, 49);
        }
    }
}
