using GameStats.Model;

namespace GameStats.Store.Interfaces;

public interface IMatchTypeStore
{
    Task<IEnumerable<MatchTypeModel>> GetMatchTypes(PagedQuery<MatchTypeModel> pagedQuery);
    Task<MatchTypeModel?> GetMatchType(int matchTypeId);
    Task<MatchTypeModel?> CreateMatchType(MatchTypeModel matchType);
    Task<MatchTypeModel?> UpdateMatchType(MatchTypeModel matchType);
    Task<bool> DeleteMatchType(int matchTypeId);
}
