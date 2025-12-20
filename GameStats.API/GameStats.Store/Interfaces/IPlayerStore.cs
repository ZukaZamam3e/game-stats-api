using GameStats.Model;

namespace GameStats.Store.Interfaces;

public interface IPlayerStore
{
    Task<DataModel<PlayerModel>> GetPlayers(PagedQuery<PlayerModel> pagedQuery);
    Task<PlayerModel?> GetPlayer(int playerId);
    Task<PlayerModel?> CreatePlayer(PlayerModel player);
    Task<PlayerModel?> UpdatePlayer(PlayerModel player);
    Task<bool> DeletePlayer(int playerId);
}
