using FastEndpoints;
using GameStats.API.Features.Match.Shared;
using GameStats.API.Features.Match.Shared.Responses;
using GameStats.API.Features.Shared.Responses;
using GameStats.Model;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.Match;

public sealed record GetMatchDataRequest
{

    [QueryParam]
    public int? MatchId { get; set; }

    [QueryParam]
    public int? OldMatchId { get; set; }

    [QueryParam]
    public int? GameId { get; set; }

    [QueryParam]
    public string? MatchName { get; set; }

    [QueryParam]
    public int? TypeCd { get; set; }

    [QueryParam]
    public int? MapId { get; set; }

    [QueryParam]
    public int? Take { get; set; }

    [QueryParam]
    public int? Offset { get; set; }
};

public class GetMatchDataEndpoint(IMatchStore matchStore) : Endpoint<GetMatchDataRequest, DataResponse<MatchResponse>>
{
    public override void Configure()
    {
        Get("/api/match/data");
        AllowAnonymous();
    }
    public override async Task HandleAsync(
        GetMatchDataRequest request,
        CancellationToken cancellationToken
        )
    {
        var pagedQuery = request.MapToPagedQuery();
        var matches = await matchStore.GetMatches(pagedQuery);
        await Send.OkAsync(matches.MapToResponse(), cancellationToken);
    }
}
