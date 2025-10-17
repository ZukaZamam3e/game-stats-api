using GameStats.API.Features.Map;
using GameStats.API.Features.MatchTypes.Shared.Responses;
using GameStats.Model;

namespace GameStats.API.Features.MatchTypes.Shared;

public static class MatchTypeMappers
{
    public static MatchTypeResponse MapToResponse(this MatchTypeModel map) => new(map.MatchTypeId, map.MatchTypeName, map.GameId);

    public static PagedQuery<MatchTypeModel> MapToPagedQuery(this GetMatchTypeDataRequest request)
    {
        var filter = new MatchTypeModel
        {
            MatchTypeId = request.MatchTypeId ?? 0,
            GameId = request.GameId ?? 0,
            MatchTypeName = request.MatchTypeName ?? string.Empty
        };

        return new PagedQuery<MatchTypeModel>(request.Take, request.Offset, filter);
    }
}
