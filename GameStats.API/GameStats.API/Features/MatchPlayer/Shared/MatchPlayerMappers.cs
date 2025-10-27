using GameStats.API.Features.MatchPlayer.Shared.Responses;
using GameStats.Model;

namespace GameStats.API.Features.MatchPlayer.Shared;

public static class MatchPlayerMappers
{
    public static MatchPlayerResponse MapToResponse(this MatchPlayerModel matchPlayer)
        => new(
            matchPlayer.MatchPlayerId,
            matchPlayer.MatchId,
            matchPlayer.MatchTeamId,
            matchPlayer.PlayerId,
            matchPlayer.Score
        );
    
    public static IEnumerable<MatchPlayerResponse> MapToResponse(this IEnumerable<MatchPlayerModel> matchPlayers)
        => matchPlayers.Select(mp => mp.MapToResponse());

    public static PagedQuery<MatchPlayerModel> MapToPagedQuery(this GetMatchPlayerDataRequest request)
    {
        var filter = new MatchPlayerModel
        {
            MatchPlayerId = request.MatchPlayerId ?? 0,
            MatchId = request.MatchId ?? 0,
            MatchTeamId = request.MatchTeamId ?? 0,
            PlayerId = request.PlayerId ?? 0,
            Score = request.Score ?? 0
        };

        return new PagedQuery<MatchPlayerModel>(request.Take, request.Offset, filter);
    }
}
