using Ludo.UI.EventArg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.UI.Class
{
    public class Dice : Control
    {
        public DiceRenderer DiceRenderer = new DiceRenderer();

        public delegate void EventHandler(object sender, DiceRollEventArgs e);
        public event EventHandler DiceRolled;
        
        private bool canDiceBeRolled;
        public bool CanDiceBeRolled
        {
            get
            {
                return canDiceBeRolled;
            }
            set
            {
                canDiceBeRolled = value;

                if (value)
                {
                    DiceRenderer.Enabled = true;
                }
                else
                {
                    DiceRenderer.Enabled = false;
                }
            }
        }

        public Dice(bool canDiceBeRolled)
        {
            CanDiceBeRolled = canDiceBeRolled;
            DiceRenderer.Click += new System.EventHandler(this.dice_Click);
        }

        public void Roll()
        {
            if (CanDiceBeRolled)
            {
                Random random = new Random();
                int diceValue = random.Next(1, 7);
                DiceRenderer.Text = diceValue.ToString();
                CanDiceBeRolled = false;

                if (DiceRolled != null)
                {
                    DiceRolled(this, new DiceRollEventArgs
                    {
                        DiceValue = diceValue
                    });
                }
            }
        }

        private void dice_Click(object sender, EventArgs e)
        {
            Roll();
        }
    }
}