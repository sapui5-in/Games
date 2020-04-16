using Ludo.UI.Class.Controls;
using Ludo.UI.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ludo.UI.Class
{
    public class Game
    {
        private bool IsGameOver = false;
        public GameBoard GameBoard;
        public GameBoardForm GameBoardForm;
        public Player CurrentPlayer;
        public Player[] Players = new Player[4];
        private Dice Dice;

        public Game(GameBoard gameBoard, GameBoardForm gameBoardForm)
        {
            GameBoard = gameBoard;
            GameBoardForm = gameBoardForm;
            Dice = new Dice();
            Dice.DiceRenderer.Click += new System.EventHandler(this.dice_Click);
        }

        private void dice_Click(object sender, EventArgs e)
        {
            int diceValue = Dice.Roll();

            Piece piece = CurrentPlayer.Pieces[Convert.ToInt32(0)];

            CurrentPlayer.MovePiece(piece, diceValue);
            Ghor ghor = piece.GameBoardPosition.Quadrant.GetGhorByPosition(piece.GameBoardPosition.Ghor.Position);
            ghor.Controls.Add(piece);
            piece.BringToFront();


            CurrentPlayer = this.GetNextPlayer();

                

            //Ghor ghor = CurrentPlayer.Quadrant.GetGhorByPosition(2);

            //ghor.Controls.Add(Players[0].Pieces[0]);
            
        }

        public void Start()
        {

            Player player1 = new Player(Color.Red);
            Player player2 = new Player(Color.Blue);
            //Game.Start();
            AddPlayer(player1);
            AddPlayer(player2);
            this.CurrentPlayer = player1;

            // Check If minimum 2 players and max 4 players
            if (this.Players.Length >= 2 && this.Players.Length <= 4)
            {
                //while (!IsGameOver)
                //{
                //    // Player Red (P1: Q-Red, Ghor-5  P2:Q-Red, Ghor-5    P3:Q-Red, Ghor-5    P4:Q-Red, Ghor-5)
                //    //this.ShowAllPlayerInfo();

                //    int diceValue = this.Dice.Roll();

                //    Console.WriteLine($"\nCurrent Player: {CurrentPlayer.Color}\t\tDiceValue: {diceValue}");
                //    Console.Write("Which Piece to Move: ");
                //    string pieceNumber = Console.ReadLine();

                //    if (pieceNumber != "")
                //    {
                //        CurrentPlayer.MovePiece(CurrentPlayer.Pieces[Convert.ToInt32(pieceNumber)], diceValue);
                //        CurrentPlayer = this.GetNextPlayer();
                //    }
                //    continue;
                //}

                // Start the Game
                // Allow The Dice to Roll

                GameBoardForm.Controls.Add(Dice.DiceRenderer);
                Dice.DiceRenderer.Location = new System.Drawing.Point(800, 10);
                Dice.Roll();
            }
        }

        public void AddPlayer(Player player)
        {
            if (this.Players.Length <= 4)
            {
                player.Game = this;
                for (int i = 0; i < 4; i++)
                {
                    if (this.Players[i] == null)
                    {
                        this.Players[i] = player;
                        this.AssignQuadrantToPlayer(player);

                        // Set Pieces in GhorHome
                        for (int j = 0; j < player.Pieces.Length; j++)
                        {
                            Ghor ghor = player.Quadrant.QuadrantHome.GetGhorByPosition(j);

                            ghor.Controls.Add(player.Pieces[j]);
                            player.Pieces[j].BringToFront();
                        }
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
                    player.Quadrant = this.GameBoard.Quadrants[0];
                    break;
                case Enum.Color.Green:
                    player.Quadrant = this.GameBoard.Quadrants[1];
                    break;
                case Enum.Color.Blue:
                    player.Quadrant = this.GameBoard.Quadrants[2];
                    break;
                case Enum.Color.Yellow:
                    player.Quadrant = this.GameBoard.Quadrants[3];
                    break;
            }
        }

        public void End() { }

        private Player GetNextPlayer()
        {
            Quadrant currentQuadrant = CurrentPlayer.Quadrant;
            while (true)
            {
                Quadrant nextQuadrant = this.GameBoard.GetNextQuadrant(currentQuadrant);

                for (int i = 0; i < this.Players.Length; i++)
                {
                    if (this.Players[i] != null && this.Players[i].Quadrant == nextQuadrant)
                    {
                        return this.Players[i];
                    }
                    else
                    {
                        currentQuadrant = nextQuadrant;
                        continue;
                    }
                }
            }
        }
    }
}