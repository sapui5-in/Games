using Ludo.UI.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.UI.Class.Controls
{
    public class QuadrantHome : Control
    {
        public Color Color;
        public QuadrantHomeRenderer UIControl;
        public Ghor[] GhorPositions =
        {
            new Ghor(0, GhorType.Home),
            new Ghor(1, GhorType.Home),
            new Ghor(2, GhorType.Home),
            new Ghor(3, GhorType.Home),
        };

        public QuadrantHome(Color color)
        {
            Color = color;
        }

        private void Renderer()
        {
            UIControl = new QuadrantHomeRenderer(this);
        }

        public Ghor GetGhorByPosition(int position)
        {
            foreach (Ghor ghor in GhorPositions)
            {
                if (ghor.Position == position)
                {
                    return ghor;
                }
            }

            return null;
        }
    }
}