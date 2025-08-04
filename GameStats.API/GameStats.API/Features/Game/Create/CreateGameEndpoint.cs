using FluentValidation;
using GameStats.API.Abstract;
using GameStats.Data.Entities;
using GameStats.Model;
using GameStats.Store.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace GameStats.API.Features.Game.Create;

public sealed record CreateGameRequest(
    string GameName
    );

public class CreateGameEndpoint : IEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapPost("/api/game", Handle);
    }
    private static async Task<IResult> Handle(
        [FromBody] CreateGameRequest request,
        IValidator<CreateGameRequest> validator,
        [FromServices] IGameStore gameStore,
        CancellationToken cancellationToken
        )
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var game = await gameStore.CreateGame(new GameModel { GameName = request.GameName });

        if (game == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(game);
    }
}
