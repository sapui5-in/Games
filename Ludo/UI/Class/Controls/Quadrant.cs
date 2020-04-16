using Ludo.UI.Class.Controls;
using Ludo.UI.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.UI.Class
{
    public class Quadrant : Control
    {
        public int QuadrantPosition;
        public int BoardSize;
        public QuadrantRenderer QuadrantRenderer;
        public QuadrantHome QuadrantHome;
        public Ghor[] GhorPath = {
                new Ghor(0, GhorType.Normal),
                new Ghor(1, GhorType.Star),
                new Ghor(2, GhorType.Normal),
                new Ghor(3, GhorType.Normal),
                new Ghor(4, GhorType.Normal),
                new Ghor(5, GhorType.Normal),
                new Ghor(6, GhorType.Normal),
                new Ghor(7, GhorType.Normal),
                new Ghor(8, GhorType.Normal),
                new Ghor(9, GhorType.Normal),
                new Ghor(10, GhorType.Normal),
                new Ghor(11, GhorType.Normal),          // Last Ghor before next Quadrant
                new Ghor(12, GhorType.Normal),
                new Ghor(13, GhorType.FinalLine),
                new Ghor(14, GhorType.FinalLine),
                new Ghor(15, GhorType.FinalLine),
                new Ghor(16, GhorType.FinalLine),
                new Ghor(17, GhorType.FinalLine),        // Last Ghor,
                new Ghor(18, GhorType.FinalLine)
            };
        public Color Color;

        public Quadrant(int position, int boardSize)
        {
            QuadrantPosition = position;
            BoardSize = boardSize;
            if (position == 0)
            {
                Color = Color.Red;
            }
            else if (position == 1)
            {
                Color = Color.Green;
            }
            else if (position == 2)
            {
                Color = Color.Blue;
            }
            else
            {
                Color = Color.Yellow;
            }
            QuadrantHome = new QuadrantHome(Color);
            this.Renderer();
        }

        public Ghor GetGhorByPosition(int position)
        {
            foreach (Ghor ghor in GhorPath)
            {
                if (ghor.Position == position)
                {
                    return ghor;
                }
            }

            return null;
        }

        public Ghor GetFirstGhor()
        {
            return GhorPath[0];
        }

        public Ghor GetLastGhor()
        {
            return GhorPath[GhorPath.Length - 1];
        }

        private int GetGhorPosition(int position)
        {
            return position;
        }

        private void Renderer()
        {
            QuadrantRenderer = new QuadrantRenderer(this);
        }
    }
}
