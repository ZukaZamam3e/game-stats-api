using FluentValidation;
using GameStats.API.Abstract;
using GameStats.API.Common;
using GameStats.Model;
using GameStats.Store.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameStats.API.Features.Game;

public sealed record CreateGameRequest(
    string GameName
    );

public class CreateGameEndpoint : IEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapPost("/api/game/create", Handle)
            .AddEndpointFilter<ValidationFilter<CreateGameRequest>>();
    }

    private static async Task<IResult> Handle(
        [FromBody] CreateGameRequest request,
        [FromServices] IGameStore gameStore,
        CancellationToken cancellationToken
        )
    {
        var game = await gameStore.CreateGame(new GameModel { GameName = request.GameName });

        if (game == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(game);
    }
}
