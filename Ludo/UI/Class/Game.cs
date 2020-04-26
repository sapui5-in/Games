using Ludo.UI.Class.Controls;
using Ludo.UI.Enum;
using Ludo.UI.EventArg;
using System;
using System.Windows.Forms;

namespace Ludo.UI.Class
{
    public class Game
    {
        private GameFlow GameFlow;
        public GameBoardForm GameBoardForm;
        public Player[] Players = new Player[4];
        private Dice Dice = new Dice(false);

        private Timer TransitionTimer = new Timer
        {
            Interval = 20
        };

        public Game(GameBoardForm gameBoardForm)
        {
            GameBoardForm = gameBoardForm;
            Dice.DiceRolled += this.dice_Rolled;
            TransitionTimer.Tick += (sender1, e1) => MyElapsedMethod(sender1, e1);
        }

        public void Start()
        {
            Player player1 = new Player(Color.Red);
            Player player2 = new Player(Color.Blue);

            GameFlow = new GameFlow(Dice, GameBoardForm, this.Players);

            //Game.Start();
            AddPlayerToGame(player1);
            AddPlayerToGame(player2);

            // Check If minimum 2 players and max 4 players
            if (this.Players.Length >= 2 && this.Players.Length <= 4)
            {
                GameBoardForm.Controls.Add(Dice.UIControl);
                AddBtn();
                Dice.UIControl.Location = new System.Drawing.Point(800, 10);
            }
            GameFlow.Start();
        }

        private void AddBtn()
        {
            Button btn = new Button();
            btn.Click += (sender, e) => Transition(sender, e);
            btn.Text = "Start Transition";
            btn.Location = new System.Drawing.Point(800, 100);
            GameBoardForm.Controls.Add(btn);
        }

        private void Transition(object sender, EventArgs e)
        {
            System.Drawing.Point point = GameBoardForm.PointToScreen(Players[0].Pieces[0].UIControl.Location);

            Players[0].Pieces[0].UIControl.Location = point;
            GameBoardForm.Controls.Add(Players[0].Pieces[0].UIControl);
            Players[0].Pieces[0].UIControl.BringToFront();
            TransitionTimer.Start();
        }

        public void MyElapsedMethod(object sender, EventArgs e)
        {
            //System.Drawing.Point point = GameBoardForm.PointToScreen(Players[0].Pieces[0].UIControl.Location);
            System.Drawing.Point point = Players[0].Pieces[0].UIControl.Location;
            int x = point.X;
            int y = point.Y + 1;
            Players[0].Pieces[0].UIControl.Location = new System.Drawing.Point(x, y);
        }

        public void End() { }

        private void AddPlayerToGame(Player player)
        {
            player.PieceClicked += this.PieceClicked;
            player.PieceMoved += this.PieceMoved;

            if (this.Players.Length <= 4)
            {
                player.Game = this;
                for (int i = 0; i < 4; i++)
                {
                    if (this.Players[i] == null)
                    {
                        this.Players[i] = player;
                        this.AssignQuadrantToPlayer(player);
                        break;
                    }
                }
            }
        }

        private void AssignQuadrantToPlayer(Player player)
        {
            switch (player.Color)
            {
                case Enum.Color.Red:
                    player.Quadrant = this.GameBoardForm.Quadrants[0];
                    break;
                case Enum.Color.Green:
                    player.Quadrant = this.GameBoardForm.Quadrants[1];
                    break;
                case Enum.Color.Blue:
                    player.Quadrant = this.GameBoardForm.Quadrants[2];
                    break;
                case Enum.Color.Yellow:
                    player.Quadrant = this.GameBoardForm.Quadrants[3];
                    break;
            }
        }

        private void dice_Rolled(object sender, DiceRollEventArgs e)
        {
            if (GameFlow != null)
            {
                GameFlow.DiceRolled(e.DiceValue);
            }
        }

        private void PieceClicked(object sender, PieceClickEventArgs e)
        {
            if (GameFlow != null)
            {
                GameFlow.PieceClicked(e.Piece);
            }
        }

        private void PieceMoved(object sender, PiecePositionChangedEventArgs e)
        {
            if (GameFlow != null)
            {
                GameFlow.PieceMoved(e);
            }
        }
    }
}