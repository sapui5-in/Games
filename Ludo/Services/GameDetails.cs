using Ludo.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.Services
{
    public class GameDetails
    {
        public Game Game;
        public List<GamePlayer> GamePlayers;
        public List<GamePlayerPiecePosition> GamePlayerPiecePositions;
        public GameProgress GameProgress;

        public GameDetails(Game game, List<GamePlayer> gamePlayers,
            List<GamePlayerPiecePosition> gamePlayerPiecePositions, GameProgress gameProgress)
        {
            Game = game;
            GamePlayers = gamePlayers;
            GamePlayerPiecePositions = gamePlayerPiecePositions;
            GameProgress = gameProgress;
        }
    }
}
