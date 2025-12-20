using FastEndpoints;
using GameStats.API.Features.Game.Shared;
using GameStats.API.Features.Game.Shared.Responses;
using GameStats.API.Features.Shared.Responses;
using GameStats.Model;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.Game;

public sealed record GetGameDataRequest 
{
    [QueryParam]
    public int? GameId { get; set; }

    [QueryParam]
    public string? GameName { get; set; }

    [QueryParam]
    public int? Take { get; set; }

    [QueryParam]
    public int? Offset { get; set; }
};

public class GetGameDataEndpoint(IGameStore gameStore) : Endpoint<GetGameDataRequest, DataResponse<GameResponse>>
{
    public override void Configure()
    {
        Get("/api/game/data");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        GetGameDataRequest request,
        CancellationToken cancellationToken
        )
    {
        DataModel<GameModel> games = await gameStore.GetGames(request.MapToPagedQuery());

        await Send.OkAsync(games.MapToResponse(), cancellationToken);
    }
}
