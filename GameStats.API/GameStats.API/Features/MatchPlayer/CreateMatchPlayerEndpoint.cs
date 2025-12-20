using FastEndpoints;
using GameStats.API.Features.MatchPlayer.Shared;
using GameStats.API.Features.MatchPlayer.Shared.Responses;
using GameStats.Model;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.MatchPlayer;

public sealed record class CreateMatchPlayerRequest
{
    public int MatchId { get; set; }
    public int? MatchTeamId { get; set; }
    public int PlayerId { get; set; }
    public int Score { get; set; }
}

public class CreateMatchPlayerEndpoint(IMatchPlayerStore matchPlayerStore) : Endpoint<CreateMatchPlayerRequest, MatchPlayerResponse>
{
    public override void Configure()
    {
        Post("/api/matchplayer/create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        CreateMatchPlayerRequest request,
        CancellationToken cancellationToken
        )
    {
        var matchPlayer = await matchPlayerStore.CreateMatchPlayer(new MatchPlayerModel
        {
            MatchId = request.MatchId,
            MatchTeamId = request.MatchTeamId,
            PlayerId = request.PlayerId,
            Score = request.Score
        });
        if (matchPlayer == null)
        {
            await Send.ErrorsAsync(400, cancellationToken);
        }
        else
        {
            await Send.OkAsync(matchPlayer.MapToResponse(), cancellationToken);
        }
    }
}
