using GameStats.API.Features.Player.Shared.Responses;
using GameStats.Model;

namespace GameStats.API.Features.Player.Shared;

public static class PlayerMappers
{
    public static PlayerResponse MapToResponse(this PlayerModel player) => new(player.PlayerId, player.PlayerName, player.GameId, player.Emblem);

    public static PagedQuery<PlayerModel> MapToPagedQuery(this GetPlayerDataRequest request)
    {
        var filter = new PlayerModel
        {
            PlayerId = request.PlayerId ?? 0,
            PlayerName = request.PlayerName ?? string.Empty,
            GameId = request.GameId ?? 0
        };

        return new PagedQuery<PlayerModel>(request.Take, request.Offset, filter);
    }
}
