using FastEndpoints;
using GameStats.API.Features.MatchPlayer.Shared;
using GameStats.API.Features.MatchPlayer.Shared.Responses;
using GameStats.API.Features.Shared.Responses;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.MatchPlayer;

public sealed record GetMatchPlayerDataRequest
{
    [QueryParam]
    public int? MatchPlayerId { get; set; }

    [QueryParam]
    public int? MatchId { get; set; }

    [QueryParam]
    public int? PlayerId { get; set; }

    [QueryParam]
    public int? Take { get; set; }

    [QueryParam]
    public int? Offset { get; set; }

    [QueryParam]
    public int? MatchTeamId { get; set; }

    [QueryParam]
    public int? Score { get; set; }

};

public class GetMatchPlayerDataEndpoint(IMatchPlayerStore matchPlayerStore) : Endpoint<GetMatchPlayerDataRequest, DataResponse<MatchPlayerResponse>>
{
    public override void Configure()
    {
        Get("/api/matchplayer/data");
        AllowAnonymous();
    }
    public override async Task HandleAsync(
        GetMatchPlayerDataRequest request,
        CancellationToken cancellationToken
        )
    {
        var pagedQuery = request.MapToPagedQuery();
        var matchPlayers = await matchPlayerStore.GetMatchPlayers(pagedQuery);
        await Send.OkAsync(matchPlayers.MapToResponse(), cancellationToken);
    }
}
