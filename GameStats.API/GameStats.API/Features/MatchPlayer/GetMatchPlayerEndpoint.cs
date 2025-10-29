using FastEndpoints;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.MatchPlayer;

public sealed record GetMatchPlayerRequest
{
    [QueryParam]
    public int MatchPlayerId { get; set; }
}

public class GetMatchPlayerEndpoint(IMatchPlayerStore matchPlayerStore) : Endpoint<GetMatchPlayerRequest>
{
    public override void Configure()
    {
        Get("/api/matchplayer");
        AllowAnonymous();
    }
    public override async Task HandleAsync(
        GetMatchPlayerRequest request,
        CancellationToken cancellationToken
        )
    {
        var matchPlayer = await matchPlayerStore.GetMatchPlayer(request.MatchPlayerId);
        if (matchPlayer == null)
        {
            await Send.NotFoundAsync(cancellationToken);
        }
        else
        {
            await Send.OkAsync(matchPlayer, cancellationToken);
        }
    }
}
