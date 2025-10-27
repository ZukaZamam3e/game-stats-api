using FastEndpoints;
using GameStats.API.Features.Map.Shared;
using GameStats.API.Features.Map.Shared.Responses;
using GameStats.Model;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.Map;

public sealed record UpdateMapRequest
{
    public int MapId { get; set; }
    public required string MapName { get; set; }
    public int GameId { get; set; }
}

public class UpdateMapEndpoint(IMapStore mapStore) : Endpoint<UpdateMapRequest, MapResponse>
{
    public override void Configure()
    {
        Post("/api/map/update");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateMapRequest request, CancellationToken cancellationToken)
    {
        var map = await mapStore.UpdateMap(new MapModel
        {
            MapId = request.MapId,
            MapName = request.MapName,
            GameId = request.GameId
        });

        if (map == null)
            await Send.NotFoundAsync(cancellationToken);
        else
            await Send.OkAsync(map.MapToResponse(), cancellationToken);
    }
}