using GameStats.API.Features.MatchTeam.Shared.Responses;
using GameStats.Model;


namespace GameStats.API.Features.MatchTeam.Shared;

public static class MatchTeamMappers
{
    public static MatchTeamResponse MapToResponse(this MatchTeamModel matchTeam) 
        => new(matchTeam.MatchTeamId, matchTeam.MatchId, matchTeam.TeamColor);

    public static IEnumerable<MatchTeamResponse> MapToResponse(this IEnumerable<MatchTeamModel> matchTeams)
        => matchTeams.Select(m => m.MapToResponse());

    public static PagedQuery<MatchTeamModel> MapToPagedQuery(this GetMatchTeamDataRequest request)
    {
        var filter = new MatchTeamModel
        {
            MatchTeamId = request.MatchTeamId ?? 0,
            MatchId = request.MatchId ?? 0,
            TeamColor = request.TeamColor ?? string.Empty
        };

        return new PagedQuery<MatchTeamModel>(request.Take, request.Offset, filter);
    }
}
