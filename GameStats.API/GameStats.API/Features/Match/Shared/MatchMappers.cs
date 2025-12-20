using GameStats.API.Features.Game.Shared;
using GameStats.API.Features.Game.Shared.Responses;
using GameStats.API.Features.Match.Shared.Responses;
using GameStats.API.Features.Shared.Responses;
using GameStats.Model;

namespace GameStats.API.Features.Match.Shared;

public static class MatchMappers
{
    public static MatchResponse MapToResponse(this MatchModel match)
        => new(
            match.MatchId,
            match.OldMatchId,
            match.GameId,
            match.MatchName,
            match.TypeCd,
            match.MapId,
            match.TotalTime,
            match.CreatedDate
        );

    public static IEnumerable<MatchResponse> MapToResponse(this IEnumerable<MatchModel> matches)
        => matches.Select(m => m.MapToResponse());

    public static DataResponse<MatchResponse> MapToResponse(this DataModel<MatchModel> data) 
        => new(data.Data.MapToResponse(), data.Count);

    public static PagedQuery<MatchModel> MapToPagedQuery(this GetMatchDataRequest request)
    {
        var filter = new MatchModel
        {
            MatchId = request.MatchId ?? 0,
            OldMatchId = request.OldMatchId ?? 0,
            GameId = request.GameId ?? 0,
            MatchName = request.MatchName ?? string.Empty,
            TypeCd = request.TypeCd ?? 0,
            MapId = request.MapId ?? 0,
        };

        return new PagedQuery<MatchModel>(request.Take, request.Offset, filter);
    }
}
