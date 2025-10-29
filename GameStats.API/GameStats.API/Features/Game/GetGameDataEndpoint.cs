using FastEndpoints;
using GameStats.API.Features.Game.Shared;
using GameStats.API.Features.Game.Shared.Responses;
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

public sealed record GetGameDataResponse(IEnumerable<GameResponse> Games);

public class GetGameDataEndpoint(IGameStore gameStore) : Endpoint<GetGameDataRequest, GetGameDataResponse>
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
        IEnumerable<GameModel> games = await gameStore.GetGames(request.MapToPagedQuery()) ?? [];

        await Send.OkAsync(new GetGameDataResponse(games.MapToResponse()), cancellationToken);
    }
}
