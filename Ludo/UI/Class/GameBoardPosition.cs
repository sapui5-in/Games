using Ludo.UI.Class;
using Ludo.UI.Class.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.UI.Class
{
    public class GameBoardPosition
    {
        public Quadrant Quadrant;
        public Ghor Ghor;

        public GameBoardPosition()
        {

        }

        public GameBoardPosition(Quadrant quadrant, Ghor ghor)
        {
            Quadrant = quadrant;
            Ghor = ghor;
        }
    }
}
