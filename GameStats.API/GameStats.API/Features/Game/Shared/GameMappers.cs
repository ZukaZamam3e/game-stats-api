using GameStats.API.Features.Game.Shared.Responses;
using GameStats.API.Features.MatchTeam.Shared;
using GameStats.API.Features.MatchTeam.Shared.Responses;
using GameStats.API.Features.Shared.Responses;
using GameStats.Model;
using System.Runtime.CompilerServices;

namespace GameStats.API.Features.Game.Shared;

public static class GameMappers
{
    public static GameResponse MapToResponse(this GameModel game) => new(game.GameId, game.GameName);

    public static IEnumerable<GameResponse> MapToResponse(this IEnumerable<GameModel> games)
        => games.Select(game => game.MapToResponse());

    public static DataResponse<GameResponse> MapToResponse(this DataModel<GameModel> data) => new(data.Data.MapToResponse(), data.Count);

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
