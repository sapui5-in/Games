using Ludo.UI.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.UI.Utils
{
    public static class Util
    {
        public static System.Drawing.Color GetDrawingColor(Color color)
        {
            System.Drawing.Color col = System.Drawing.Color.Red;

            if (color == Enum.Color.Red)
            {
                col = System.Drawing.Color.Red;
            }
            else if (color == Enum.Color.Green)
            {
                col = System.Drawing.Color.Green;
            }
            else if (color == Enum.Color.Blue)
            {
                col = System.Drawing.Color.Blue;
            }
            else
            {
                col = System.Drawing.Color.Yellow;
            }

            return col;
        }
    }
}
