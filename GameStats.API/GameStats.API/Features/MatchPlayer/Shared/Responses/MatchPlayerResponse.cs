namespace GameStats.API.Features.MatchPlayer.Shared.Responses;

public sealed record MatchPlayerResponse(
        int MatchPlayerId,
        int MatchId,
        int? MatchTeamId,
        int PlayerId,
        int Score
    );
