using GameStats.Model;

namespace GameStats.Store.Interfaces;

public interface IMatchPlayerStore
{
    Task<IEnumerable<MatchPlayerModel>> GetMatchePlayers(PagedQuery<MatchPlayerModel> pagedQuery);
    Task<MatchPlayerModel?> GetMatchPlayer(int matchPlayerId);
    Task<MatchPlayerModel?> CreateMatchPlayer(MatchPlayerModel matchPlayer);
    Task<MatchPlayerModel?> UpdateMatchPlayer(MatchPlayerModel matchPlayer);
    Task<bool> DeleteMatchPlayer(int matchPlayerId);
}
