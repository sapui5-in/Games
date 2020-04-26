using Ludo.UI.Class.Controls;
using Ludo.UI.Enum;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Ludo.UI.Class
{
    public partial class GameBoardForm : Form
    {
        public bool Flag = true;
        public int BoardSize = 750;
        public Quadrant[] Quadrants = new Quadrant[4];

        public GameBoardForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            this.SuspendLayout();
            this.CreateQuadrants();
            this.ResumeLayout(false);

            Game game = new Game(this);
            game.Start();
        }

        private void CreateQuadrants()
        {
            for (int i = 0; i < Quadrants.Length; i++)
            {
                Quadrants[i] = new Quadrant(i, BoardSize);
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.Controls.Add(Quadrants[i].UIControl.Container);
            }
        }

        public Quadrant GetNextQuadrant(Quadrant quadrant)
        {
            switch (quadrant.Color)
            {
                case Color.Red:
                    return Quadrants[1];
                case Color.Green:
                    return Quadrants[2];
                case Color.Blue:
                    return Quadrants[3];
                case Color.Yellow:
                    return Quadrants[0];
                default:
                    return null;
            }
        }

        public GameBoardPosition GetNextGhor(GameBoardPosition gameBoardPosition, Player player)
        {
            if (gameBoardPosition != null)
            {

                Ghor ghor = gameBoardPosition.Ghor;
                Quadrant quadrant = gameBoardPosition.Quadrant;

                if (ghor != null && ghor.Position == 18)
                {
                    return null;
                }
                else if (ghor.GhorType == GhorType.Home)
                {
                    // Move to Home Star
                    gameBoardPosition.Ghor = quadrant.GhorPath[1];
                }
                else if (ghor.Position == 11)
                {
                    //Move to Next Quadrant
                    gameBoardPosition.Quadrant = this.GetNextQuadrant(quadrant);
                    gameBoardPosition.Ghor = gameBoardPosition.Quadrant.GhorPath[12];
                }
                else if (ghor.Position == 12)
                {
                    if (gameBoardPosition.Quadrant.Color == player.Color)
                    {
                        // Move to Final Line
                        gameBoardPosition.Ghor = gameBoardPosition.Quadrant.GhorPath[13];
                    }
                    else
                    {
                        gameBoardPosition.Ghor = gameBoardPosition.Quadrant.GhorPath[0];
                    }
                }
                else if (ghor == quadrant.GetLastGhor())
                {
                    // Matured
                    gameBoardPosition.Ghor = new Ghor(18);
                }
                else
                {
                    // Proceed One ghor
                    gameBoardPosition.Ghor = quadrant.GhorPath[ghor.Position + 1];
                }

                return gameBoardPosition;
            }
            else
            {
                return null;
            }
        }

        public List<GameBoardPosition> GetNthGhorPosition(
            GameBoardPosition gameBoardPosition, int count, Player player, List<GameBoardPosition> positions)
        {
            bool flag = false;

            if (count >= 1 && count <= 6)
            {
                for (int i = 0; i < count; i++)
                {
                    gameBoardPosition = this.GetNextGhor(gameBoardPosition, player);
                    if (gameBoardPosition == null)
                    {
                        flag = true;
                        break;
                    }
                    positions.Add(gameBoardPosition);
                }
            }
            if (flag)
            {
                positions.Clear();
            }

            return positions;
        }
    }
}
