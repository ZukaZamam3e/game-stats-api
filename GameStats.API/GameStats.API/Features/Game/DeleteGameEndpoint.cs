using GameStats.API.Abstract;
using GameStats.API.Common;
using GameStats.API.Features.Shared.Responses;
using GameStats.Store.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameStats.API.Features.Game;

public sealed record DeleteGameRequest(
    int GameId
    );

public class DeleteGameEndpoint : IEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapPost("/api/game/delete", Handle)
            .AddEndpointFilter<ValidationFilter<DeleteGameRequest>>();
    }

    private static async Task<IResult> Handle(
        [FromBody] DeleteGameRequest request,
        [FromServices] IGameStore gameStore,
        CancellationToken cancellationToken
        )
    {
        var success = await gameStore.DeleteGame(request.GameId);

        return Results.Ok(new DeleteResponse(success));
    }
}
