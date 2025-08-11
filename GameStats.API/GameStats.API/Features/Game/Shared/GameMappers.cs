using GameStats.API.Features.Game.Shared.Responses;
using GameStats.Model;

namespace GameStats.API.Features.Game.Shared;

public static class GameMappers
{
    public static GameResponse MapToResponse(this GameModel game)
     => new(game.GameId, game.GameName);
}
