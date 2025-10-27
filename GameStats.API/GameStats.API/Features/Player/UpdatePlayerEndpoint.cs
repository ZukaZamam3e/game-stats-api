using FastEndpoints;
using GameStats.API.Features.Player.Shared;
using GameStats.API.Features.Player.Shared.Responses;
using GameStats.Model;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.Player;

public sealed record UpdatePlayerRequest
{
    public required int PlayerId { get; set; }

    public required int GameId { get; set; }

    public required string PlayerName { get; set; }

    public required string Emblem { get; set; }
};

public class UpdatePlayerEndpoint(IPlayerStore playerStore) : Endpoint<UpdatePlayerRequest, PlayerResponse>
{
    public override void Configure()
    {
        Post("/api/player/update");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        UpdatePlayerRequest request,
        CancellationToken cancellationToken
        )
    {
        var player = await playerStore.UpdatePlayer(new PlayerModel { PlayerId = request.PlayerId, PlayerName = request.PlayerName, GameId = request.GameId, Emblem = request.Emblem });

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
