using Ludo.UI.Class;

namespace Ludo.Services
{
    public interface IGameService
    {
        ServiceResponse Create();

        ServiceResponse Start();

        ServiceResponse DiceRolled(int gameId);

        ServiceResponse UpdateCurrentPlayer(int gameId, int diceValue);

        ServiceResponse MovePiece(int gameId, int currentPlayerId, int pieceNumber, GameBoardPosition position, int nextPlayerId);

        ServiceResponse GameOver(int gameId);
    }
}
