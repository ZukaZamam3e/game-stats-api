using FastEndpoints;
using GameStats.API.Features.Shared.Responses;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.MatchTeam;

public sealed record DeleteMatchTeamRequest
{
    public int MatchTeamId { get; set; }
};

public class DeleteMatchTeamEndpoint(IMatchTeamStore matchTeamStore) : Endpoint<DeleteMatchTeamRequest, DeleteResponse>
{
    public override void Configure()
    {
        Post("/api/matchteam/delete");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        DeleteMatchTeamRequest request,
        CancellationToken cancellationToken
        )
    {
        var success = await matchTeamStore.DeleteMatchTeam(request.MatchTeamId);
        await Send.OkAsync(new DeleteResponse(success), cancellationToken);
    }
}
