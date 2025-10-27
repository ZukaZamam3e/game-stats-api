using FastEndpoints;
using GameStats.API.Features.Match.Shared;
using GameStats.API.Features.Match.Shared.Responses;
using GameStats.Model;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.Match;

public sealed record UpdateMatchRequest
{
    public int MatchId { get; set; }
    public int OldMatchId { get; set; }
    public int GameId { get; set; }
    public required string MatchName { get; set; }
    public int TypeCd { get; set; }
    public int MapId { get; set; }
    public int? TotalTime { get; set; }
    public DateTime? CreatedDate { get; set; }
};

public class UpdateMatchEndpoint(IMatchStore matchStore) : Endpoint<UpdateMatchRequest, MatchResponse>
{
    public override void Configure()
    {
        Post("/api/match/update");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        UpdateMatchRequest request,
        CancellationToken cancellationToken
        )
    {
        var match = await matchStore.UpdateMatch(new MatchModel 
        { 
            MatchId = request.MatchId,
            OldMatchId = request.OldMatchId,
            GameId = request.GameId,
            MatchName = request.MatchName,
            TypeCd = request.TypeCd,
            MapId = request.MapId,
            TotalTime = request.TotalTime,
            CreatedDate = request.CreatedDate
        });

        if (match == null)
        {
            await Send.NotFoundAsync(cancellationToken);
        }
        else
        {
            await Send.OkAsync(match.MapToResponse(), cancellationToken);
        }
    }
}
