using GameStats.Model;

namespace GameStats.Store.Interfaces;

public interface IGameStore
{
    Task<GameModel?> GetGame(int gameId);

    Task<GameModel?> CreateGame(GameModel game);

    Task<GameModel?> UpdateGame(GameModel game);   

    Task DeleteGame(int gameId);
}
