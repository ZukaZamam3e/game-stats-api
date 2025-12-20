using FastEndpoints;
using GameStats.API.Features.Shared.Responses;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.MatchPlayer;

public sealed record DeleteMatchPlayerRequest
{
   public int MatchPlayerId { get; set; }
};

public class DeleteMatchPlayerEndpoint(IMatchPlayerStore matchPlayerStore) : Endpoint<DeleteMatchPlayerRequest, DeleteResponse>
{
    public override void Configure()
    {
        Post("/api/matchplayer/delete");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        DeleteMatchPlayerRequest request,
        CancellationToken cancellationToken
        )
    {
        var success = await matchPlayerStore.DeleteMatchPlayer(request.MatchPlayerId);
        await Send.OkAsync(new DeleteResponse(success), cancellationToken);
    }
}
