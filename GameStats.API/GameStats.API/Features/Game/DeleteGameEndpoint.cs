using FastEndpoints;
using GameStats.API.Features.Shared.Responses;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.Game;

public sealed record DeleteGameRequest
{
    public int GameId { get; set; }
};

public class DeleteGameEndpoint(IGameStore gameStore) : Endpoint<DeleteGameRequest, DeleteResponse>
{
    public override void Configure()
    {
        Post("/api/game/delete");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        DeleteGameRequest request,
        CancellationToken cancellationToken
        )
    {
        var success = await gameStore.DeleteGame(request.GameId);

        await Send.OkAsync(new DeleteResponse(success), cancellationToken);
    }
}
