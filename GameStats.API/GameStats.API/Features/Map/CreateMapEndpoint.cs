using FastEndpoints;
using GameStats.API.Features.Map.Shared;
using GameStats.API.Features.Map.Shared.Responses;
using GameStats.Model;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.Map;

public sealed record CreateMapRequest
{
    public required string MapName { get; set; }
    public int GameId { get; set; }
}

public class CreateMapEndpoint(IMapStore mapStore) : Endpoint<CreateMapRequest, MapResponse>
{
    public override void Configure()
    {
        Post("/api/map/create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateMapRequest request, CancellationToken cancellationToken)
    {
        var map = await mapStore.CreateMap(new MapModel
        {
            MapName = request.MapName,
            GameId = request.GameId
        });

        if (map == null)
            await Send.ErrorsAsync(400, cancellationToken);
        else
            await Send.OkAsync(map.MapToResponse(), cancellationToken);
    }
}