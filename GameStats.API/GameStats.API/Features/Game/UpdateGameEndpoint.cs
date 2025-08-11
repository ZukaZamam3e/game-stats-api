using GameStats.API.Abstract;
using GameStats.API.Common;
using GameStats.Model;
using GameStats.Store.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace GameStats.API.Features.Game;

public sealed record UpdateGameRequest(
    int GameId,
    string GameName
    );

public class UpdateGameEndpoint : IEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapPost("/api/game/update", Handle)
            .AddEndpointFilter<ValidationFilter<UpdateGameRequest>>();
    }

    private static async Task<IResult> Handle(
        [FromBody] UpdateGameRequest request,
        [FromServices] IGameStore gameStore,
        CancellationToken cancellationToken
        )
    {
        var game = await gameStore.UpdateGame(new GameModel { GameId = request.GameId, GameName = request.GameName });

        if (game == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(game);
    }
}
