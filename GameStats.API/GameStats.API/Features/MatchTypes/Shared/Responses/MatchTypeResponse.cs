namespace GameStats.API.Features.MatchTypes.Shared.Responses;

public sealed record MatchTypeResponse(
    int MatchTypeId,
    string MatchTypeName,
    int GameId
);