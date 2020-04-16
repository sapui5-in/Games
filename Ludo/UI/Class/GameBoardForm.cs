using Ludo.UI.Class.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ludo.UI.Class
{
    public partial class GameBoardForm : Form
    {
        public int BoardSize = 750;

        public GameBoardForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            GameBoard gameBoard = new GameBoard();

            this.SuspendLayout();
            for (int i = 0; i < gameBoard.Quadrants.Length; i++)
            {
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.Controls.Add(gameBoard.Quadrants[i].QuadrantRenderer.Container);
            }
            this.ResumeLayout(false);

            Game game = new Game(gameBoard, this);
            game.Start();
        }
    }
}
