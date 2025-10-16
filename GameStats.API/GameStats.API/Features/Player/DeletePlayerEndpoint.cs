using FastEndpoints;
using GameStats.API.Features.Player.Shared.Responses;
using GameStats.API.Features.Shared.Responses;
using GameStats.Model;
using GameStats.Store;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.Player;

public sealed record DeletePlayerRequest
{
    public int PlayerId { get; set; }
};

public class DeletePlayerEndpoint(IPlayerStore playerStore) : Endpoint<DeletePlayerRequest, DeleteResponse>
{
    public override void Configure()
    {
        Post("/api/player/delete");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        DeletePlayerRequest request,
        CancellationToken cancellationToken
        )
    {
        var success = await playerStore.DeletePlayer(request.PlayerId);

        await Send.OkAsync(new DeleteResponse(success), cancellationToken);
    }
}
