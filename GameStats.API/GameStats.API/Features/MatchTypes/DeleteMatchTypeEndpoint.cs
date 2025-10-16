using FastEndpoints;
using GameStats.API.Features.Map;
using GameStats.API.Features.Shared.Responses;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.MatchTypes;

public sealed record DeleteMatchTypeRequest
{
    public int MatchTypeId { get; set; }
}

public class DeleteMatchTypeEndpoint(IMatchTypeStore matchTypeStore) : Endpoint<DeleteMatchTypeRequest, DeleteResponse>
{
    public override void Configure()
    {
        Post("/api/matchtype/delete");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteMatchTypeRequest request, CancellationToken cancellationToken)
    {
        var success = await matchTypeStore.DeleteMatchType(request.MatchTypeId);

        await Send.OkAsync(new DeleteResponse(success), cancellationToken);
    }
}
