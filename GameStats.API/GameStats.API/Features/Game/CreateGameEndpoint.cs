using FastEndpoints;
using GameStats.API.Features.Game.Shared;
using GameStats.API.Features.Game.Shared.Responses;
using GameStats.Model;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.Game;

public sealed record CreateGameRequest
{
    public required string GameName { get; set; }
};

public class CreateGameEndpoint(IGameStore gameStore) : Endpoint<CreateGameRequest, GameResponse>
{
    public override void Configure()
    {
        Post("/api/game/create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        CreateGameRequest request,
        CancellationToken cancellationToken
        )
    {
        var game = await gameStore.CreateGame(new GameModel { GameName = request.GameName });

        if (game == null)
        {
            await Send.ErrorsAsync(400, cancellationToken);
        }
        else
        {
            await Send.OkAsync(game.MapToResponse(), cancellationToken);
        }
    }
}
