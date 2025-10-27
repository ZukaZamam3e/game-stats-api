using FastEndpoints;
using GameStats.API.Features.MatchTeam.Shared;
using GameStats.API.Features.MatchTeam.Shared.Responses;
using GameStats.Model;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.MatchTeam;

public sealed record UpdateMatchTeamRequest
{
    public int MatchTeamId { get; set; }
    public int MatchId { get; set; }
    public required string TeamColor { get; set; }
}

public class UpdateMatchTeamEndpoint(IMatchTeamStore matchTeamStore) : Endpoint<UpdateMatchTeamRequest, MatchTeamResponse>
{
    public override void Configure()
    {
        Post("api/matchteam/update");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        UpdateMatchTeamRequest request, 
        CancellationToken cancellationToken)
    {
        var matchTeam = await matchTeamStore.UpdateMatchTeam(new MatchTeamModel
        {
            MatchTeamId = request.MatchTeamId,
            MatchId = request.MatchId,
            TeamColor = request.TeamColor,
        });

        if (matchTeam == null)
        {
            await Send.NotFoundAsync(cancellationToken);
        }
        else
        {
            await Send.OkAsync(matchTeam.MapToResponse(), cancellationToken);
        }
    }
}
