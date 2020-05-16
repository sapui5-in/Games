using Ludo.API.Model;
using Ludo.Models;
using Ludo.UI.Class.Controls;
using Ludo.UI.Enum;
using Ludo.UI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ludo.Services
{
    public class GameService : IGameService
    {
        private LudoContext LudoContext { get; }

        public User CreaterByUserId { get; set; }
        public string GameType { get; set; }
        public Game Game { get; set; }

        // Inject DBContext into the Constructor
        public GameService(LudoContext ludoContext)
        {
            LudoContext = ludoContext;
        }

        public ServiceResponse Create()
        {
            Game = new Game
            {
                GameType = "Basic",
                CreatedByUserId = GetCreateByUserId(),
                GameStatusId = 1
            };

            Game.CreatedDateTime = DateTime.Now;

            LudoContext.Games.Add(Game);
            LudoContext.SaveChanges();

            AddPlayer(GetCreateByUserId(), 0);

            return new ServiceResponse
            {
                Data = Game,
                Messages = new List<Message>()
            };
        }

        private int GetCreateByUserId()
        {
            return 1;
        }

        public void AddPlayer(int playerId, int quadrant)
        {
            LudoContext.GamePlayers.Add(
                new GamePlayer
                {
                    GameId = Game.Id,
                    PlayerId = playerId,
                    Quadrant = quadrant
                });

            LudoContext.SaveChanges();
        }

        // Add this only Just before Game start as Players may Switch Colors
        public void AddGamePlayerPiecePosition(List<GamePlayerPiecePosition> gamePlayerPiecePositions, int playerId, int quadrant)
        {
            for (int i = 0; i < 4; i++)
            {
                gamePlayerPiecePositions.Add(new GamePlayerPiecePosition
                {
                    GameId = Game.Id,
                    PlayerId = playerId,
                    PieceNumber = i,
                    GhorPosition = i,
                    GhorType = "Home",
                    Quadrant = quadrant
                });
            }
        }

        public ServiceResponse Start()
        {
            int currentPlayerId = 0;
            // Assign positions to Pieces - GamePlayerPiecePosition
            List<GamePlayer> gamePlayers = LudoContext.GamePlayers
                .Where(c => c.GameId == Game.Id).ToList();

            List<GamePlayerPiecePosition> gamePlayerPiecePositions = new List<GamePlayerPiecePosition>();

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                AddGamePlayerPiecePosition(gamePlayerPiecePositions, gamePlayer.PlayerId, gamePlayer.Quadrant);

                if (gamePlayer.Quadrant == 0)
                {
                    currentPlayerId = gamePlayer.PlayerId;
                }
            }

            LudoContext.GamePlayerPiecePositions.AddRange(gamePlayerPiecePositions);

            // Change GameStatusId - Games
            LudoContext.Games
                .Single(c => c.Id == Game.Id)
                .GameStatusId = 2;

            // Create New Entry in GameProgress
            GameProgress gameProgress = new GameProgress
            {
                GameId = Game.Id,
                CurrentPlayerId = currentPlayerId,
                CurrentPlayerDiceRolled = false,
                LastActionDateTime = DateTime.Now
            };
            LudoContext.GameProgresses.Add(gameProgress);

            LudoContext.DiceStacks.Add(new DiceStack
            {
                GameId = Game.Id,
                DiceValue1 = -1,
                DiceValue2 = -1,
                DiceValue3 = -1
            });

            GameDetails gameDetails = new GameDetails(Game, gamePlayers, gamePlayerPiecePositions, gameProgress);

            LudoContext.SaveChanges();

            return new ServiceResponse
            {
                Data = gameDetails,
                Messages = new List<Message>()
            };
        }

        public ServiceResponse GetGameDetails(int gameId)
        {
            Game game = LudoContext.Games.Single(c => c.Id == gameId);

            List<GamePlayer> gamePlayers = LudoContext.GamePlayers.Where(c => c.GameId == gameId).ToList();

            List<GamePlayerPiecePosition> gamePlayerPiecePositions =
                LudoContext.GamePlayerPiecePositions.Where(c => c.GameId == gameId).ToList();

            GameProgress gameProgress = LudoContext.GameProgresses.Single(c => c.GameId == gameId);

            GameDetails gameDetails = new GameDetails(game, gamePlayers, gamePlayerPiecePositions, gameProgress);

            return new ServiceResponse
            {
                Data = gameDetails,
                Messages = new List<Message>()
            };
        }

        public ServiceResponse DiceRolled(int gameId)
        {
            int diceValue = GetDiceRandomValue();

            GameProgress gameProgress = LudoContext.GameProgresses
                .Single(c => c.GameId == gameId);

            gameProgress.CurrentPlayerDiceRolled = true;
            gameProgress.LastDiceValue = diceValue;
            gameProgress.LastActionDateTime = DateTime.Now;

            DiceStack diceStack = LudoContext.DiceStacks
                .Single(c => c.GameId == gameId);

            if (diceStack.DiceValue1 == -1)
            {
                diceStack.DiceValue1 = diceValue;
                diceStack.DiceValue2 = -1;
                diceStack.DiceValue3 = -1;
            }
            else if (diceStack.DiceValue2 == -1)
            {
                diceStack.DiceValue2 = diceValue;
                diceStack.DiceValue3 = -1;
            }
            else if (diceStack.DiceValue3 == -1)
            {
                diceStack.DiceValue3 = diceValue;
            }
            else
            {
                diceStack.DiceValue1 = diceValue;
                diceStack.DiceValue2 = -1;
                diceStack.DiceValue3 = -1;
            }

            // Check if Current Player has any move, else assign the next player as Current Player

            LudoContext.SaveChanges();

            return new ServiceResponse
            {
                Data = diceValue,
                Messages = new List<Message>()
            };
        }

        private int GetDiceRandomValue()
        {
            Random random = new Random();
            int diceValue = random.Next(1, 7);

            return diceValue;
        }

        public ServiceResponse UpdateCurrentPlayer(int gameId, int playerId)
        {
            GameProgress gameProgress = LudoContext.GameProgresses.Single(c => c.GameId == gameId);
            gameProgress.CurrentPlayerId = playerId;
            gameProgress.CurrentPlayerDiceRolled = false;

            // Clear Dice Stack
            DiceStack diceStack = LudoContext.DiceStacks
                .Single(c => c.GameId == gameId);
            diceStack.DiceValue1 = -1;
            diceStack.DiceValue2 = -1;
            diceStack.DiceValue3 = -1;

            LudoContext.SaveChanges();

            return new ServiceResponse
            {
                Data = gameProgress,
                Messages = new List<Message>()
            };
        }

        public ServiceResponse MovePiece(int gameId, int currentPlayerId, int pieceNumber,
            Ludo.UI.Class.GameBoardPosition position, int nextPlayerId)
        {
            GamePlayerPiecePosition gamePlayerPiecePosition =
            LudoContext.GamePlayerPiecePositions.Single(c => c.GameId == gameId && c.PlayerId
                == currentPlayerId && c.PieceNumber == pieceNumber);

            gamePlayerPiecePosition.GhorPosition = position.Ghor.Position;
            gamePlayerPiecePosition.GhorType = Util.GetGhorTypeFromEnum(position.Ghor.GhorType);
            gamePlayerPiecePosition.Quadrant = position.Quadrant.QuadrantPosition;

            GameProgress gameProgress = LudoContext.GameProgresses.Single(c => c.GameId == gameId);

            // Here check the Dice Stack
            DiceStack diceStack = LudoContext.DiceStacks
                .Single(c => c.GameId == gameId);

            if (gameProgress.LastDiceValue == 6)
            {
                if (diceStack.DiceValue3 == 6)
                {
                    gameProgress.CurrentPlayerId = nextPlayerId;
                    diceStack.DiceValue1 = -1;
                    diceStack.DiceValue2 = -1;
                    diceStack.DiceValue3 = -1;
                    gameProgress.CurrentPlayerDiceRolled = false;
                }
                else
                {
                    gameProgress.CurrentPlayerDiceRolled = false;
                }
            }
            else
            {
                gameProgress.CurrentPlayerId = nextPlayerId;
                diceStack.DiceValue1 = -1;
                diceStack.DiceValue2 = -1;
                diceStack.DiceValue3 = -1;
                gameProgress.CurrentPlayerDiceRolled = false;
            }
            gameProgress.LastActionDateTime = DateTime.Now;

            //Take opponent pice
            List<GamePlayerPiecePosition> otherPlayerPiecePositions =
                LudoContext.GamePlayerPiecePositions.Where(c => c.GameId == gameId && c.PlayerId
                != currentPlayerId).ToList();

            foreach (GamePlayerPiecePosition piecePosition in otherPlayerPiecePositions)
            {
                if (piecePosition.GhorPosition == gamePlayerPiecePosition.GhorPosition
                    && piecePosition.Quadrant == gamePlayerPiecePosition.Quadrant)
                {
                    GamePlayer gamePlayer = LudoContext.GamePlayers
                        .Single(c => c.GameId == gameId && c.PlayerId == piecePosition.PlayerId);

                    piecePosition.Quadrant = gamePlayer.Quadrant;
                    piecePosition.GhorPosition = piecePosition.PieceNumber;
                    piecePosition.GhorType = Util.GetGhorTypeFromEnum(GhorType.Home);

                    // Continue Current Player turn
                    gameProgress.CurrentPlayerId = currentPlayerId;
                    gameProgress.CurrentPlayerDiceRolled = false;

                }
            }

            LudoContext.SaveChanges();

            return new ServiceResponse
            {
                Data = gamePlayerPiecePosition,
                Messages = new List<Message>()
            };
        }

        public ServiceResponse GameOver(int gameId)
        {
            Game game = LudoContext.Games.Single(c => c.Id == gameId);
            game.GameStatusId = 3;

            LudoContext.SaveChanges();

            return new ServiceResponse
            {
                Data = game,
                Messages = new List<Message>()
            };
        }

        public ServiceResponse TakeOpponentPiece(int gameId, int opponentPlayerId, int pieceNumber)
        {
            GamePlayerPiecePosition gamePlayerPiecePosition =
            LudoContext.GamePlayerPiecePositions.Single(c => c.GameId == gameId && c.PlayerId
                == opponentPlayerId && c.PieceNumber == pieceNumber);

            GamePlayer gamePlayer = LudoContext.GamePlayers.Single(c => c.GameId == gameId && c.PlayerId == opponentPlayerId);

            gamePlayerPiecePosition.GhorPosition = pieceNumber;
            gamePlayerPiecePosition.GhorType = Util.GetGhorTypeFromEnum(GhorType.Home);
            gamePlayerPiecePosition.Quadrant = gamePlayer.Quadrant;

            LudoContext.SaveChanges();

            return new ServiceResponse
            {
                Data = gamePlayerPiecePosition,
                Messages = new List<Message>()
            };
        }
    }
}