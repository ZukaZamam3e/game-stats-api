using GameStats.Model;

namespace GameStats.Store.Interfaces;

public interface IGameStore
{
    Task<DataModel<GameModel>> GetGames(PagedQuery<GameModel> pagedQuery);

    Task<GameModel?> GetGame(int gameId);

    Task<GameModel?> CreateGame(GameModel game);

    Task<GameModel?> UpdateGame(GameModel game);   

    Task<bool> DeleteGame(int gameId);
}
