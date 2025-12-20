using FastEndpoints;
using GameStats.API.Features.Map.Shared;
using GameStats.API.Features.Map.Shared.Responses;
using GameStats.API.Features.Shared.Responses;
using GameStats.Model;
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

public class GetMapDataEndpoint(IMapStore mapStore) : Endpoint<GetMapDataRequest, DataResponse<MapResponse>>
{
    public override void Configure()
    {
        Get("/api/map/data");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetMapDataRequest request, CancellationToken cancellationToken)
    {
        DataModel<MapModel> maps = await mapStore.GetMaps(request.MapToPagedQuery());

        await Send.OkAsync(maps.MapToResponse(), cancellationToken);
    }
}