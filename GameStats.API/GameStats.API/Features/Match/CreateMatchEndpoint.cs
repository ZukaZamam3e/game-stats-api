using FastEndpoints;
using GameStats.API.Features.Match.Shared;
using GameStats.API.Features.Match.Shared.Responses;
using GameStats.Model;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.Match;

public sealed record CreateMatchRequest
{
    public int OldMatchId { get; set; }
    public int GameId { get; set; }
    public required string MatchName { get; set; }
    public int TypeCd { get; set; }
    public int MapId { get; set; }
    public int? TotalTime { get; set; }
    public DateTime? CreatedDate { get; set; }
}

public class CreateMatchEndpoint(IMatchStore matchStore) : Endpoint<CreateMatchRequest, MatchResponse>
{
    public override void Configure()
    {
        Post("/api/match/create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        CreateMatchRequest request,
        CancellationToken cancellationToken
        )
    {
        var match = await matchStore.CreateMatch(new MatchModel 
        { 
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
            await Send.ErrorsAsync(400, cancellationToken);
        }
        else
        {
            await Send.OkAsync(match.MapToResponse(), cancellationToken);
        }
    }
}
