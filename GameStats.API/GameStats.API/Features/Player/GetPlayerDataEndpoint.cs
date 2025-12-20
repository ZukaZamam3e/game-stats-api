using FastEndpoints;
using GameStats.API.Features.Game.Shared;
using GameStats.Model;
using GameStats.Store.Interfaces;
using GameStats.API.Features.Player.Shared;
using GameStats.API.Features.Player.Shared.Responses;
using GameStats.API.Features.Shared.Responses;

namespace GameStats.API.Features.Player;

public sealed record GetPlayerDataRequest
{
    [QueryParam]
    public int? PlayerId { get; set; }

    [QueryParam]
    public int? GameId { get; set; }

    [QueryParam]
    public string? PlayerName { get; set; }

    [QueryParam]
    public int? Take { get; set; }

    [QueryParam]
    public int? Offset { get; set; }
};

public class GetPlayerDataEndpoint(IPlayerStore playerStore) : Endpoint<GetPlayerDataRequest, DataResponse<PlayerResponse>>
{
    public override void Configure()
    {
        Get("/api/player/data");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        GetPlayerDataRequest request,
        CancellationToken cancellationToken
        )
    {
        DataModel<PlayerModel> players = await playerStore.GetPlayers(request.MapToPagedQuery());

        await Send.OkAsync(players.MapToResponse(), cancellationToken);
    }
}
