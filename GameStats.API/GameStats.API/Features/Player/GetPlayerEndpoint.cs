using FastEndpoints;
using GameStats.API.Features.Player.Shared;
using GameStats.API.Features.Player.Shared.Responses;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.Player;

public sealed record GetPlayerRequest
{
    [QueryParam]
    public int PlayerId { get; set; }
};

public class GetPlayerEndpoint(IPlayerStore playerStore) : Endpoint<GetPlayerRequest, PlayerResponse>
{
    public override void Configure()
    {
        Get("/api/player/get");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        GetPlayerRequest request,
        CancellationToken cancellationToken
        )
    {
        var player = await playerStore.GetPlayer(request.PlayerId);

        if (player == null)
        {
            await Send.NotFoundAsync(cancellationToken);
        }
        else
        {
            await Send.OkAsync(player.MapToResponse(), cancellationToken);
        }
    }
}
