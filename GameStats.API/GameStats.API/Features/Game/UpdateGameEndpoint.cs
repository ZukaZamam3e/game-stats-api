using FastEndpoints;
using GameStats.API.Features.Game.Shared;
using GameStats.API.Features.Game.Shared.Responses;
using GameStats.Model;
using GameStats.Store.Interfaces;
namespace GameStats.API.Features.Game;

public sealed record UpdateGameRequest
{
    public int GameId {get;set;}
    public required string GameName { get; set; }
};

public class UpdateGameEndpoint(IGameStore gameStore) : Endpoint<UpdateGameRequest, GameResponse>
{
    public override void Configure()
    {
        Post("/api/game/update");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        UpdateGameRequest request,
        CancellationToken cancellationToken
        )
    {
        var game = await gameStore.UpdateGame(new GameModel { GameId = request.GameId, GameName = request.GameName });

        if (game == null)
        {
            await Send.NotFoundAsync(cancellationToken);
        }
        else
        {
            await Send.OkAsync(game.MapToResponse(), cancellationToken);
        }
    }
}
