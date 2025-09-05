using FastEndpoints;
using GameStats.API.Features.Game.Shared;
using GameStats.API.Features.Game.Shared.Responses;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.Game;

public sealed record GetGameRequest 
{
    [QueryParam]
    public int GameId { get; set; }
};

public class GetGameEndpoint(IGameStore gameStore) : Endpoint<GetGameRequest, GameResponse>
{
    public override void Configure()
    {
        Get("/api/game/get");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        GetGameRequest request,
        CancellationToken cancellationToken
        )
    {
        var game = await gameStore.GetGame(request.GameId);

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
