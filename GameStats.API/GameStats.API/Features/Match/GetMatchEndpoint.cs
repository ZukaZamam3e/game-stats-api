using FastEndpoints;
using GameStats.API.Features.Match.Shared;
using GameStats.API.Features.Match.Shared.Responses;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.Match;

public sealed record GetMatchRequest
{
    [QueryParam]
    public int MatchId { get; set; }
};

public class GetMatchEndpoint(IMatchStore matchStore): Endpoint<GetMatchRequest, MatchResponse>
{
    public override void Configure()
    {
        Get("/api/match/get");
        AllowAnonymous();
    }
    public override async Task HandleAsync(
        GetMatchRequest request,
        CancellationToken cancellationToken
        )
    {
        var match = await matchStore.GetMatch(request.MatchId);
        if (match == null)
        {
            await Send.NotFoundAsync(cancellationToken);
        }
        else
        {
            await Send.OkAsync(match.MapToResponse(), cancellationToken);
        }
    }
}
