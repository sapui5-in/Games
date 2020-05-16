using Ludo.API.Model;
using Ludo.Services;
using Ludo.UI.Class.Controls;
using Ludo.UI.Enum;
using Ludo.UI.EventArg;
using Ludo.UI.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Ludo.UI.Class
{
    public class Game
    {
        public List<Player> Players = new List<Player>();
        public GameBoardForm GameBoardForm;
        private GameDetails GameDetails;
        private Dice Dice;
        private int GameId;
        public Player CurrentPlayer;
        public List<int> DiceStack = new List<int>();

        public Game(GameBoardForm gameBoardForm)
        {
            Dice = gameBoardForm.Dice;
            Dice.DiceRolled += this.dice_Rolled;
            GameBoardForm = gameBoardForm;
        }

        private Player GetPlayerById(int currentPlayerId)
        {
            return Players.Find(c => c.Id == currentPlayerId);
        }

        public void Start(int gameId)
        {
            GameId = gameId;
            GameDetails = GetGameDetails(GameId);

            // 2. Add Players to Game
            AddPlayers();

            Load();
        }

        private void AddPlayers()
        {
            List<GamePlayer> gamePlayers = GameDetails.GamePlayers;

            for (int i = 0; i < gamePlayers.Count; i++)
            {
                Player player = new Player(gamePlayers[i].PlayerId, Utils.Util.GetColorFromQuadrant(gamePlayers[i].Quadrant));

                player.Game = this;
                Players.Add(player);
                player.Quadrant = GameBoardForm.Quadrants[i];

                player.PieceClicked += PieceClicked;
                player.PieceMoved += PieceMoved;
            }
        }

        public void End() { }

        public void Load()
        {
            // 1. Fetch Game Details
            GameDetails = GetGameDetails(GameId);

            // 3. Add Player Piece Position
            AddPlayerPieces();

            // 4. SetCurrentPlayer
            SetCurrentPlayer(GetPlayerById(GameDetails.GameProgress.CurrentPlayerId));

            // 5. Update Dice Value
            UpdateDiceStatus();

            // 6. Assign turn to Player and Decide if he should Roll Dice or Move
            SetGameFlow();
        }

        // 1. Fetch Game Details
        private GameDetails GetGameDetails(int gameId)
        {
            using (LudoContext ludoContext = new LudoContext())
            {
                GameService gameService = new GameService(ludoContext);

                ServiceResponse response = gameService.GetGameDetails(gameId);

                return (response.Data as GameDetails);
            }
        }

        // 3. Add Player Piece Position
        public void AddPlayerPieces()
        {
            List<GamePlayerPiecePosition> gamePlayerPiecePositions = GameDetails.GamePlayerPiecePositions;

            for (int i = 0; i < Players.Count; i++)
            {
                foreach (GamePlayerPiecePosition gamePlayerPiecePosition in gamePlayerPiecePositions)
                {
                    if (gamePlayerPiecePosition.PlayerId == Players[i].Id)
                    {
                        SetPiecePosition(Players[i], gamePlayerPiecePosition);
                    }
                }
            }
        }

        // 4. SetCurrentPlayer
        private void SetPiecePosition(Player player, GamePlayerPiecePosition gamePlayerPiecePosition)
        {
            Quadrant quadrant = GameBoardForm.Quadrants[gamePlayerPiecePosition.Quadrant];
            Ghor ghor;

            if (gamePlayerPiecePosition.GhorType == "Home")
            {
                ghor = quadrant.QuadrantHome.GhorPositions[gamePlayerPiecePosition.GhorPosition];
            }
            else
            {
                ghor = quadrant.GetGhorByPosition(gamePlayerPiecePosition.GhorPosition);
            }

            try
            {
                Piece piece = player.Pieces[gamePlayerPiecePosition.PieceNumber];
                piece.Movable = false;
                //player.Pieces[gamePlayerPiecePosition.PieceNumber].TransitionPositions = new List<GameBoardPosition>
                //{
                //    new GameBoardPosition(quadrant, ghor)
                //};
                //ShowTransitions(player.Pieces[gamePlayerPiecePosition.PieceNumber]);
                piece.GameBoardPosition = new GameBoardPosition(quadrant, ghor);

                ghor.UIControl.Controls.Add(piece.UIControl);
                piece.UIControl.BringToFront();
            }
            catch (Exception) { }
        }

        // 5. Update Dice Value
        public void SetCurrentPlayer(Player player)
        {
            DiceStack.Clear();

            if (CurrentPlayer != null)
            {
                CurrentPlayer.Quadrant.UIControl.Active = false;
            }
            CurrentPlayer = player;
            CurrentPlayer.Quadrant.UIControl.Active = true;
        }

        // 6. Assign turn to Player and Decide if he should Roll Dice or Move
        private void SetGameFlow()
        {
            if (GameDetails.GameProgress.CurrentPlayerDiceRolled)
            {
                List<PiecePosition> piecePositions = GetAllPiecesValidMoves();

                // Check if Current Player has any move
                if (piecePositions.Count > 1)
                {
                    HighlightPiecesOfCurrentPlayer();
                }
                else if (piecePositions.Count == 1)
                {
                    MovePiece(piecePositions[0].Piece);
                }
                else
                {
                    Player nextPlayer = this.GetNextPlayer();
                    if (nextPlayer != null)
                    {
                        using (LudoContext ludoContext = new LudoContext())
                        {
                            GameService gameService = new GameService(ludoContext);
                            gameService.UpdateCurrentPlayer(GameDetails.Game.Id, nextPlayer.Id);
                        }
                    }
                    Load();
                }
            }
        }

        private void UpdateDiceStatus()
        {
            Dice.DiceValue = GameDetails.GameProgress.LastDiceValue;
            Dice.CanDiceBeRolled = !GameDetails.GameProgress.CurrentPlayerDiceRolled;
            Dice.UIControl.BackColor = Util.GetDrawingColor(CurrentPlayer.Color);
        }

        private void HighlightPiecesOfCurrentPlayer()
        {
            foreach (Player player in Players)
            {
                foreach (Piece piece in player.Pieces)
                {
                    if (player.Id != CurrentPlayer.Id)
                    {
                        piece.Movable = false;
                    }
                    else if (CheckValidMove(player, piece).Count > 0)
                    {
                        piece.Movable = true;
                    }
                    else
                    {
                        piece.Movable = false;
                    }
                }
            }
        }

        // Get Valid Moves of all Pieces
        private List<PiecePosition> GetAllPiecesValidMoves()
        {
            List<PiecePosition> piecePositions = new List<PiecePosition>();
            foreach (Piece piece in CurrentPlayer.Pieces)
            {
                List<GameBoardPosition> positions = CheckValidMove(CurrentPlayer, piece);
                if (positions.Count > 0)
                {
                    piecePositions.Add(new PiecePosition(piece, positions));
                }
            }

            return piecePositions;
        }

        private void PieceClicked(object sender, PieceClickEventArgs e)
        {
            MovePiece(e.Piece);
        }

        private bool TakeOpponentPiece(Piece piece)
        {
            foreach (Player player in Players)
            {
                if (CurrentPlayer != null && player != null && player != CurrentPlayer)
                {
                    foreach (Piece piece1 in player.Pieces)
                    {
                        if (piece1.GameBoardPosition.Ghor.Position == piece.GameBoardPosition.Ghor.Position
                            && piece1.GameBoardPosition.Quadrant.QuadrantPosition == piece.GameBoardPosition.Quadrant.QuadrantPosition)
                        {
                            Quadrant quadrant = player.Quadrant;
                            piece1.GameBoardPosition.Quadrant = quadrant;
                            piece1.GameBoardPosition.Ghor = quadrant.QuadrantHome.GhorPositions[piece1.Position];

                            piece1.TransitionPositions = new List<GameBoardPosition> {
                                piece1.GameBoardPosition
                            };

                            ShowTransitions(piece1);
                            Load();
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
            List<GameBoardPosition> positions = this.CheckValidMove(CurrentPlayer, piece);

            if (positions.Count > 0)
            {
                Player nextPlayer = GetNextPlayer();
                if (nextPlayer != null)
                {
                    using (LudoContext ludoContext = new LudoContext())
                    {
                        GameService gameService = new GameService(ludoContext);
                        gameService.MovePiece(GameId, CurrentPlayer.Id,
                            piece.Position, positions[positions.Count - 1], nextPlayer.Id);
                    }
                }
                piece.GameBoardPosition = positions[positions.Count - 1];
                piece.TransitionPositions = positions;
                ShowTransitions(piece);
            }
        }

        public List<GameBoardPosition> CheckValidMove(Player player, Piece piece)
        {
            List<GameBoardPosition> positions = new List<GameBoardPosition>();

            bool flag = true;
            int diceValue = Dice.DiceValue;

            if (piece.GameBoardPosition.Ghor.Position != 18)
            {
                // From Home, only 6 can make him move to Start Star
                if (piece.GameBoardPosition.Ghor.GhorType == GhorType.Home)
                {
                    if (Dice.DiceValue == 6)
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
                    positions =
                        GameBoardForm.GetNthGhorPosition(currentPiecePosition, diceValue, player, positions);
                }
            }

            return positions;
        }

        private Player GetNextPlayer()
        {
            if (CurrentPlayer != null)
            {
                Quadrant currentQuadrant = CurrentPlayer.Quadrant;
                while (true)
                {
                    Quadrant nextQuadrant = this.GameBoardForm.GetNextQuadrant(currentQuadrant);

                    for (int i = 0; i < this.Players.Count; i++)
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

        private void dice_Rolled(object sender, DiceRollEventArgs e)
        {
            using (LudoContext ludoContext = new LudoContext())
            {
                GameService gameService = new GameService(ludoContext);

                ServiceResponse response = gameService.DiceRolled(GameId);
            }
            Load();
        }

        public void ShowTransitions(Piece piece)
        {

            foreach (GameBoardPosition position in piece.TransitionPositions)
            {
                Ghor ghor = position.Ghor;
                ghor.UIControl.Controls.Add(piece.UIControl);
                piece.UIControl.BringToFront();
            }
        }

        public void PieceMoved(object sender, PiecePositionChangedEventArgs e)
        {
            // Once a Piece is Moved, do some checks like
            // 1. If Opponents Guti can be Eaten
            // 2. If Game is over etc

            if (e.Piece.GameBoardPosition.Ghor.Position == 18)
            {
                Dice.CanDiceBeRolled = false;
                e.Piece.GameBoardPosition.Quadrant.UIControl.Active = false;
                // Set Action for Game Over
                MessageBox.Show("GameOver!!");
            }
            else
            {
                if (e.Piece.GameBoardPosition.Ghor.GhorType != GhorType.Home)
                {
                    if (!TakeOpponentPiece(e.Piece))
                    {
                        Load();
                    }
                } else
                {
                    Load();
                }
            }
        }
    }
}