using FastEndpoints;
using GameStats.API.Features.Shared.Responses;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.Map;

public sealed record DeleteMapRequest
{
    public int MapId { get; set; }
}

public class DeleteMapEndpoint(IMapStore mapStore) : Endpoint<DeleteMapRequest, DeleteResponse>
{
    public override void Configure()
    {
        Post("/api/map/delete");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteMapRequest request, CancellationToken cancellationToken)
    {
        var success = await mapStore.DeleteMap(request.MapId);

        await Send.OkAsync(new DeleteResponse(success), cancellationToken);
    }
}