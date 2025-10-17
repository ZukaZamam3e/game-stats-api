using GameStats.Model;

namespace GameStats.Store.Interfaces;

public interface IMatchStore
{
    Task<IEnumerable<MatchModel>> GetMatches(PagedQuery<MatchModel> pagedQuery);
    Task<MatchModel?> GetMatch(int matchId);
    Task<MatchModel?> CreateMatch(MatchModel match);
    Task<MatchModel?> UpdateMatch(MatchModel match);
    Task<bool> DeleteMatch(int matchId);
}
