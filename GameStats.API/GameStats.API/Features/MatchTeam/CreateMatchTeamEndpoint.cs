using FastEndpoints;
using GameStats.API.Features.MatchTeam.Shared;
using GameStats.API.Features.MatchTeam.Shared.Responses;
using GameStats.Model;
using GameStats.Store;
using GameStats.Store.Interfaces;
using System.Threading;

namespace GameStats.API.Features.MatchTeam;

public sealed record CreateMatchTeamRequest
{
    public int MatchId { get; set; }
    public required string TeamColor { get; set; }
}

public class CreateMatchTeamEndpoint(IMatchTeamStore matchTeamStore) : Endpoint<CreateMatchTeamRequest, MatchTeamResponse>
{
    public override void Configure()
    {
        Post("api/matchteam/create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateMatchTeamRequest request, CancellationToken cancellationToken)
    {
        var matchTeam = await matchTeamStore.CreateMatchTeam(new MatchTeamModel
        {
            MatchId = request.MatchId,
            TeamColor = request.TeamColor
        });

        if (matchTeam == null)
        {
            await Send.ErrorsAsync(400, cancellationToken);
        }
        else
        {
            await Send.OkAsync(matchTeam.MapToResponse(), cancellationToken);
        }
    }
}
