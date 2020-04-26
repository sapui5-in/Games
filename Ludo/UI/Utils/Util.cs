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

        public static System.Drawing.Color GetDarkDrawingColor(Color color)
        {
            System.Drawing.Color col;

            if (color == Enum.Color.Red)
            {
                col = System.Drawing.Color.DarkRed;
            }
            else if (color == Enum.Color.Green)
            {
                col = System.Drawing.Color.DarkGreen;
            }
            else if (color == Enum.Color.Blue)
            {
                col = System.Drawing.Color.DarkBlue;
            }
            else
            {
                col = System.Drawing.ColorTranslator.FromHtml("#dbc300");
            }

            return col;
        }
    }
}
