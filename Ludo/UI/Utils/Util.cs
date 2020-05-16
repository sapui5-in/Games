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

        public static int GetQuadrantFromColor(Color color)
        {
            if (color == Enum.Color.Red)
            {
                return 0;
            }
            else if (color == Enum.Color.Green)
            {
                return 1;
            }
            else if (color == Enum.Color.Blue)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        public static Color GetColorFromQuadrant(int quadrant)
        {
            if (quadrant == 0)
            {
                return Enum.Color.Red;
            }
            else if (quadrant == 1)
            {
                return Enum.Color.Green;
            }
            else if (quadrant == 2)
            {
                return Enum.Color.Blue;
            }
            else
            {
                return Enum.Color.Yellow;
            }
        }

        public static string GetGhorTypeFromEnum(GhorType ghorType)
        {
            if (ghorType == GhorType.Star)
            {
                return "Star";
            }
            else if (ghorType == GhorType.Home)
            {
                return "Home";
            }
            else if (ghorType == GhorType.Normal)
            {
                return "Normal";
            }
            else
            {
                return "FinalLine";
            }
        }
    }
}
