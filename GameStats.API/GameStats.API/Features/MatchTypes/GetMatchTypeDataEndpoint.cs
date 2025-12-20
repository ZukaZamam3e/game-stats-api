using FastEndpoints;
using GameStats.API.Features.MatchTypes.Shared;
using GameStats.API.Features.MatchTypes.Shared.Responses;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.MatchTypes;

public sealed record GetMatchTypeDataRequest
{
    [QueryParam]
    public int? MatchTypeId { get; set; }

    [QueryParam]
    public string? MatchTypeName { get; set; }

    [QueryParam]
    public int? GameId { get; set; }

    [QueryParam]
    public int? Take { get; set; }

    [QueryParam]
    public int? Offset { get; set; }
}

public class GetMatchTypeDataEndpoint(IMatchTypeStore matchTypeStore) : Endpoint<GetMatchTypeDataRequest>
{
    public override void Configure()
    {
        Get("/api/matchtype/data");
        AllowAnonymous();
    }
    public override async Task HandleAsync(GetMatchTypeDataRequest request, CancellationToken cancellationToken)
    {
        var matchTypes = await matchTypeStore.GetMatchTypes(request.MapToPagedQuery());

        await Send.OkAsync(matchTypes.MapToResponse(), cancellationToken);
    }
}
