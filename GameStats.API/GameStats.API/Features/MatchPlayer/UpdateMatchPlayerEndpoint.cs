using FastEndpoints;
using GameStats.API.Features.MatchPlayer.Shared;
using GameStats.API.Features.MatchPlayer.Shared.Responses;
using GameStats.Model;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.MatchPlayer;

public sealed record UpdateMatchPlayerRequest
{
    public int MatchPlayerId { get; set; }
    public int MatchId { get; set; }
    public int? MatchTeamId { get; set; }
    public int PlayerId { get; set; }
    public int Score { get; set; }
}

public class UpdateMatchPlayerEndpoint(IMatchPlayerStore matchPlayerStore) : Endpoint<UpdateMatchPlayerRequest, MatchPlayerResponse>
{
    public override void Configure()
    {
        Post("/api/matchplayer/update");
        AllowAnonymous();
    }
    public override async Task HandleAsync(
        UpdateMatchPlayerRequest request,
        CancellationToken cancellationToken
        )
    {
        var matchPlayer = await matchPlayerStore.UpdateMatchPlayer(new MatchPlayerModel
        {
            MatchPlayerId = request.MatchPlayerId,
            MatchId = request.MatchId,
            MatchTeamId = request.MatchTeamId,
            PlayerId = request.PlayerId,
            Score = request.Score
        });

        if (matchPlayer == null)
        {
            await Send.NotFoundAsync(cancellationToken);
        }
        else
        {
            await Send.OkAsync(matchPlayer.MapToResponse(), cancellationToken);
        }
    }
}
