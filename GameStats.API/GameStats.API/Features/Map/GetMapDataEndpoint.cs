using FastEndpoints;
using GameStats.API.Features.Game;
using GameStats.API.Features.Map.Shared;
using GameStats.API.Features.Map.Shared.Responses;
using GameStats.Model;
using GameStats.Store;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.Map;

public sealed record GetMapDataRequest
{
    [QueryParam]
    public int? MapId { get; set; }

    [QueryParam]
    public string? MapName { get; set; }

    [QueryParam]
    public int? GameId { get; set; }

    [QueryParam]
    public int? Take { get; set; }

    [QueryParam]
    public int? Offset { get; set; }
}

public sealed record GetMapDataResponse(IEnumerable<MapModel> Maps);

public class GetMapDataEndpoint(IMapStore mapStore) : Endpoint<GetMapDataRequest, GetMapDataResponse>
{
    public override void Configure()
    {
        Get("/api/map/getmapdata");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetMapDataRequest request, CancellationToken cancellationToken)
    {
        IEnumerable<MapModel> maps = await mapStore.GetMaps(request.MapToPagedQuery()) ?? [];

        await Send.OkAsync(new GetMapDataResponse(maps), cancellationToken);
    }
}