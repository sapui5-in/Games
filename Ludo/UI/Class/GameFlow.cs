using Ludo.UI.Class.Controls;
using Ludo.UI.Enum;
using Ludo.UI.EventArg;
using Ludo.UI.Utils;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Ludo.UI.Class
{
    public class GameFlow
    {
        public Player[] Players = new Player[4];
        public GameBoardForm GameBoardForm;
        public Dice Dice;

        public Player CurrentPlayer;
        public int DiceValue = 0;

        public GameFlow(Dice dice, GameBoardForm gameBoardForm, Player[] players)
        {
            Dice = dice;
            GameBoardForm = gameBoardForm;
            Players = players;
        }

        public void Start()
        {
            AssignDiceToPlayer(Players[0]);
            // Check Dice Value
            // Check Current Player
            // Check Available Moves
            // Check if Current Player should be changed
        }

        public void DiceRolled(int diceValue)
        {
            DiceValue = diceValue;
            List<PiecePosition> piecePositions = GetAllPiecesValidMoves();

            // Check if Current Player has any move
            if (piecePositions.Count == 1)
            {
                MovePiece(piecePositions[0].Piece);
            }
            else if (piecePositions.Count > 1)
            {
                HighlightPiecesOfCurrentPlayer();
            }
            else
            {
                AssignDiceToPlayer(GetNextPlayer());
            }
        }

        private bool DoesCurrentPlayerHasMove()
        {
            foreach (Piece piece in CurrentPlayer.Pieces)
            {
                if (CheckValidMove(CurrentPlayer, piece) != null)
                {
                    return true;
                }
            }

            return false;
        }

        // Get Valid Moves of all Pieces
        private List<PiecePosition> GetAllPiecesValidMoves()
        {
            List<PiecePosition> piecePositions = new List<PiecePosition>();
            foreach (Piece piece in CurrentPlayer.Pieces)
            {
                GameBoardPosition position = CheckValidMove(CurrentPlayer, piece);
                if (position != null)
                {
                    piecePositions.Add(new PiecePosition(piece, position));
                }
            }

            return piecePositions;
        }

        private void AssignDiceToPlayer(Player player)
        {
            if (player != null)
            {
                Dice.CanDiceBeRolled = true;
                this.SetCurrentPlayer(player);
            }
        }

        private void ChangeDiceOwner(Player newOwner)
        {
            CurrentPlayer.DisableAllPieceMovement();
            this.AssignDiceToPlayer(newOwner);
        }

        private void HighlightPiecesOfCurrentPlayer()
        {
            foreach (Piece piece in CurrentPlayer.Pieces)
            {
                if (CheckValidMove(CurrentPlayer, piece) != null)
                {
                    piece.Movable = true;
                }
                else
                {
                    piece.Movable = false;
                }
            }
        }

        public void PieceClicked(Piece piece)
        {
            this.MovePiece(piece);
        }

        public void PieceMoved(PiecePositionChangedEventArgs e)
        {
            // Once a Piece is Moved, do some checks like
            // 1. Show Transition from Old to New Position
            // 2. If Opponents Guti can be Eaten
            // 3. If Game is over etc

            if (e.Piece.GameBoardPosition != null)
            {
                Ghor ghor = e.NewPosition.Ghor;
                ghor.Controls.Add(e.Piece.PieceRenderer);
                e.Piece.PieceRenderer.BringToFront();

                if (e.Piece.GameBoardPosition.Ghor.GhorType != GhorType.Home)
                {
                    if (TakeOpponentPiece(e.Piece))
                    {
                        CurrentPlayer.DisableAllPieceMovement();
                    } else
                    {
                        if (!ContinueCurrentPlayerTurn())
                        {
                            Player nextPlayer = this.GetNextPlayer();
                            if (nextPlayer != null)
                            {
                                this.SetCurrentPlayer(nextPlayer);
                            }
                        }
                        else
                        {
                            CurrentPlayer.DisableAllPieceMovement();
                        }
                    }
                }
                Dice.CanDiceBeRolled = true;
            }
        }

        private bool TakeOpponentPiece(Piece piece)
        {
            foreach (Player player in Players)
            {
                if (CurrentPlayer != null && player != null && player != CurrentPlayer)
                {
                    foreach (Piece piece1 in player.Pieces)
                    {
                        if (piece1.GameBoardPosition.Ghor == piece.GameBoardPosition.Ghor)
                        {
                            Quadrant quadrant = player.Quadrant;
                            piece1.GameBoardPosition =
                                new GameBoardPosition(quadrant, quadrant.QuadrantHome.GhorPositions[piece1.Position]);
                            return true;
                        }
                    }
                }
            }

            return false;
        }



        private void MovePiece(Piece piece)
        {
            // Show Transition
            GameBoardPosition newPosition = this.CheckValidMove(CurrentPlayer, piece);

            if (newPosition != null)
            {
                // Show Transition

                //Move to Final Position
                piece.GameBoardPosition = newPosition;
            }
        }

        public GameBoardPosition CheckValidMove(Player player, Piece piece)
        {
            bool flag = true;
            int diceValue = DiceValue;

            if (piece.GameBoardPosition.Ghor.Position != -1)
            {
                // From Home, only 6 can make him move to Start Star
                if (piece.GameBoardPosition.Ghor.GhorType == GhorType.Home)
                {
                    if (DiceValue == 6)
                    {
                        diceValue = 1;
                    }
                    else
                    {
                        flag = false;
                    }
                }

                if (flag)
                {
                    GameBoardPosition currentPiecePosition =
                        new GameBoardPosition(piece.GameBoardPosition.Quadrant, piece.GameBoardPosition.Ghor);
                    GameBoardPosition newPosition =
                        this.GameBoardForm.GetNthGhorPosition(currentPiecePosition, diceValue, player);

                    return newPosition;
                }
            }

            return null;
        }

        private bool ContinueCurrentPlayerTurn()
        {
            if (DiceValue == 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SetCurrentPlayer(Player player)
        {
            if (CurrentPlayer != null)
            {
                CurrentPlayer.Quadrant.QuadrantRenderer.Active = false;
            }
            CurrentPlayer = player;
            CurrentPlayer.Quadrant.QuadrantRenderer.Active = true;
            Dice.DiceRenderer.BackColor = Util.GetDrawingColor(CurrentPlayer.Color);
        }

        private Player GetNextPlayer()
        {
            if (CurrentPlayer != null)
            {
                Quadrant currentQuadrant = CurrentPlayer.Quadrant;
                while (true)
                {
                    Quadrant nextQuadrant = this.GameBoardForm.GetNextQuadrant(currentQuadrant);

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

            return null;
        }
    }
}
