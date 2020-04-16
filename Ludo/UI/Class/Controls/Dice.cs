using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.UI.Class
{
    class Dice : Control
    {
        public DiceRenderer DiceRenderer = new DiceRenderer();

        public Dice()
        {

        }

        public int Roll()
        {
            Random random = new Random();
            int diceValue = random.Next(1, 7);
            DiceRenderer.Text = diceValue.ToString();

            return diceValue;
        }
    }
}