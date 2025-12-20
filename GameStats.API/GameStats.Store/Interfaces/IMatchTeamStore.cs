using GameStats.Model;

namespace GameStats.Store.Interfaces;

public interface IMatchTeamStore
{
    Task<DataModel<MatchTeamModel>> GetMatcheTeams(PagedQuery<MatchTeamModel> pagedQuery);
    Task<MatchTeamModel?> GetMatchTeam(int matchTeamId);
    Task<MatchTeamModel?> CreateMatchTeam(MatchTeamModel matchTeam);
    Task<MatchTeamModel?> UpdateMatchTeam(MatchTeamModel matchTeam);
    Task<bool> DeleteMatchTeam(int matchTeamId);
}
