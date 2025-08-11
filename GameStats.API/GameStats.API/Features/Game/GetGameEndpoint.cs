using GameStats.API.Abstract;
using GameStats.API.Features.Game.Shared;
using GameStats.Store.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameStats.API.Features.Game;

public class GetGameEndpoint : IEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapGet("/api/game/{gameId}", Handle);
    }

    private static async Task<IResult> Handle(
        [FromRoute] int gameId,
        [FromServices] IGameStore gameStore
        )
    {
        var game = await gameStore.GetGame(gameId);

        if(game == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(game.MapToResponse());
    }
}
