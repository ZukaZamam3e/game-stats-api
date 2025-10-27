using FastEndpoints;
using GameStats.API.Features.Game;
using GameStats.API.Features.Shared.Responses;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.Match;

public sealed record DeleteMatchRequest
{
    public int MatchTeamId { get; set; }
};

public class DeleteMatchEndpoint(IMatchStore matchStore) : Endpoint<DeleteMatchRequest, DeleteResponse>
{
    public override void Configure()
    {
        Post("/api/matchteam/delete");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        DeleteMatchRequest request,
        CancellationToken cancellationToken
        )
    {
        var success = await matchStore.DeleteMatch(request.MatchTeamId);

        await Send.OkAsync(new DeleteResponse(success), cancellationToken);
    }
}