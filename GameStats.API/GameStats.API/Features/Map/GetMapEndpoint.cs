using FastEndpoints;
using GameStats.API.Features.Map.Shared;
using GameStats.API.Features.Map.Shared.Responses;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.Map;

public sealed record GetMapRequest
{
    [QueryParam]
    public int MapId { get; set; }
}

public class GetMapEndpoint(IMapStore mapStore) : Endpoint<GetMapRequest, MapResponse>
{
    public override void Configure()
    {
        Get("/api/map/get");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetMapRequest request, CancellationToken cancellationToken)
    {
        var map = await mapStore.GetMap(request.MapId);

        if (map == null)
            await Send.NotFoundAsync(cancellationToken);
        else
            await Send.OkAsync(map.MapToResponse(), cancellationToken);
    }
}