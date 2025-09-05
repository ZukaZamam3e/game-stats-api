using FastEndpoints;
using GameStats.API.Features.Player.Shared;
using GameStats.API.Features.Player.Shared.Responses;
using GameStats.Model;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.Player;

public sealed record CreatePlayerRequest
{
    public required int GameId { get; set; }
    
    public required string PlayerName { get; set; }

    public required string Emblem { get; set; }
};

public class CreatePlayerEndpoint(IPlayerStore playerStore) : Endpoint<CreatePlayerRequest, PlayerResponse>
{
    public override void Configure()
    {
        Post("/api/player/create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        CreatePlayerRequest request,
        CancellationToken cancellationToken
        )
    {
        var player = await playerStore.CreatePlayer(new PlayerModel { PlayerName = request.PlayerName, GameId = request.GameId, Emblem = request.Emblem });

        if (player == null)
        {
            await Send.ErrorsAsync(400, cancellationToken);
        }
        else
        {
            await Send.OkAsync(player.MapToResponse(), cancellationToken);
        }
    }
}
