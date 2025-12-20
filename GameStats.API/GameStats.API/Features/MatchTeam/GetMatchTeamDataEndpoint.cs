using FastEndpoints;
using GameStats.API.Features.MatchTeam.Shared;
using GameStats.API.Features.MatchTeam.Shared.Responses;
using GameStats.API.Features.Shared.Responses;
using GameStats.Model;
using GameStats.Store;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.MatchTeam;

public sealed record GetMatchTeamDataRequest
{
    [QueryParam]
    public int? MatchTeamId { get; set; }

    [QueryParam]
    public int? MatchId { get; set; }

    [QueryParam]
    public string? TeamColor { get; set; }

    [QueryParam]
    public int? Take { get; set; }

    [QueryParam]
    public int? Offset { get; set; }
}

public class GetMatchTeamDataEndpoint(IMatchTeamStore matchTeamStore) : Endpoint<GetMatchTeamDataRequest, DataResponse<MatchTeamResponse>>
{
    public override void Configure()
    {
        Get("api/matchteam/data");
        AllowAnonymous();
    }
    public override async Task HandleAsync(GetMatchTeamDataRequest request, CancellationToken cancellationToken)
    {
        DataModel<MatchTeamModel> matchTeams = await matchTeamStore.GetMatcheTeams(request.MapToPagedQuery());
        
        await Send.OkAsync(matchTeams.MapToResponse(), cancellationToken);
    }
}
