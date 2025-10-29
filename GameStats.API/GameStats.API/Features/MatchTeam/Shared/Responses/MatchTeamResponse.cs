namespace GameStats.API.Features.MatchTeam.Shared.Responses;

public sealed record MatchTeamResponse
(
    int MatchTeamId,
    int MatchId,
    string TeamColor
);
