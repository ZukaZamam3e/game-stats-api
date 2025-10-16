namespace GameStats.API.Features.Map.Shared.Responses;

public sealed record MapResponse(
    int MapId,
    string MapName,
    int GameId
);