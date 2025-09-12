using GameStats.API.Features.Game;
using GameStats.API.Features.Map.Shared.Responses;
using GameStats.Model;

namespace GameStats.API.Features.Map.Shared;

public static class MapMappers
{
    public static MapResponse MapToResponse(this MapModel map) => new(map.MapId, map.MapName, map.GameId);

    public static PagedQuery<MapModel> MapToPagedQuery(this GetMapDataRequest request)
    {
        var filter = new MapModel
        {
            MapId = request.MapId ?? 0,
            GameId = request.GameId ?? 0,
            MapName = request.MapName ?? string.Empty
        };

        return new PagedQuery<MapModel>(request.Take, request.Offset, filter);
    }
}