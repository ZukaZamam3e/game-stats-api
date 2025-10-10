using FastEndpoints;
using GameStats.API.Features.MatchTypes.Shared;
using GameStats.API.Features.MatchTypes.Shared.Responses;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.MatchTypes;

public sealed record GetMatchTypeRequest
{
    [QueryParam]
    public int MatchTypeId { get; set; }
}

public class GetMatchTypeEndpoint(IMatchTypeStore matchTypeStore) : Endpoint<GetMatchTypeRequest, MatchTypeResponse>
{
    public override void Configure()
    {
        Get("/api/map/get");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetMatchTypeRequest request, CancellationToken cancellationToken)
    {
        var matchType = await matchTypeStore.GetMatchType(request.MatchTypeId);

        if (matchType == null)
            await Send.NotFoundAsync(cancellationToken);
        else
            await Send.OkAsync(matchType.MapToResponse(), cancellationToken);
    }
}
