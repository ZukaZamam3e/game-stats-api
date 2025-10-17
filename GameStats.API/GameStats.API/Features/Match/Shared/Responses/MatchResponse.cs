namespace GameStats.API.Features.Match.Shared.Responses;

public sealed record MatchResponse(
        int MatchId,
        int OldMatchId,
        int GameId,
        string MatchName,
        int TypeCd,
        int MapId,
        int? TotalTime,
        DateTime? CreatedDate
    );
