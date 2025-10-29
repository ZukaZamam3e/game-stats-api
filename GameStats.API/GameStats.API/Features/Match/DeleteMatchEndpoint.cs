using FastEndpoints;
using GameStats.API.Features.Game;
using GameStats.API.Features.Shared.Responses;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.Match;

public sealed record DeleteMatchRequest
{
    public int MatchId { get; set; }
};

public class DeleteMatchEndpoint(IMatchStore matchStore) : Endpoint<DeleteMatchRequest, DeleteResponse>
{
    public override void Configure()
    {
        Post("/api/match/delete");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        DeleteMatchRequest request,
        CancellationToken cancellationToken
        )
    {
        var success = await matchStore.DeleteMatch(request.MatchId);

        await Send.OkAsync(new DeleteResponse(success), cancellationToken);
    }
}