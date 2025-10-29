namespace GameStats.API.Features.Player.Shared.Responses;

public sealed record PlayerResponse(int PlayerId, string PlayerName, int GameId, string Emblem);