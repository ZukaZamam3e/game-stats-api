using FastEndpoints;
using GameStats.API.Features.MatchTeam.Shared.Responses;
using GameStats.Store.Interfaces;
using GameStats.API.Features.MatchTeam.Shared;

namespace GameStats.API.Features.MatchTeam;

public sealed record GetMatchTeamRequest
{
    [QueryParam]
    public int MatchTeamId { get; set; }
}

public class GetMatchTeamEndpoint(IMatchTeamStore matchTeamStore) : Endpoint<GetMatchTeamRequest, MatchTeamResponse>
{
    public override void Configure()
    {
        Get("api/matchteam/get");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        GetMatchTeamRequest request,
        CancellationToken cancellationToken
        )
    {
        var matchTeam = await matchTeamStore.GetMatchTeam(request.MatchTeamId);
        if(matchTeam == null)
        {
            await Send.NotFoundAsync(cancellationToken);
        }
        else
        {
            await Send.OkAsync(matchTeam.MapToResponse(), cancellationToken);
        }
    }
}
