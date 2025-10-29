using GameStats.API.Features.Game.Shared.Responses;
using GameStats.Model;

namespace GameStats.API.Features.Game.Shared;

public static class GameMappers
{
    public static GameResponse MapToResponse(this GameModel game) => new(game.GameId, game.GameName);

    public static IEnumerable<GameResponse> MapToResponse(this IEnumerable<GameModel> games)
        => games.Select(game => game.MapToResponse());

    public static PagedQuery<GameModel> MapToPagedQuery(this GetGameDataRequest request)
    {
        var filter = new GameModel
        {
            GameId = request.GameId ?? 0,
            GameName = request.GameName ?? string.Empty
        };

        return new PagedQuery<GameModel>(request.Take, request.Offset, filter);
    }
}
