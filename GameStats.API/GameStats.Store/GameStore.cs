using GameStats.Data.Context;
using GameStats.Data.Entities;
using GameStats.Model;
using GameStats.Store.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameStats.Store;

public class GameStore(GameStatsDbContext _context) : IGameStore
{
    public async Task<GameModel?> GetGame(int gameId)
    {
        GameModel? game = await _context.GAME
            .AsNoTracking()
            .Where(g => g.GAME_ID == gameId)
            .Select(g => new GameModel
            {
                GameId = g.GAME_ID,
                GameName = g.GAME_NAME
            })
            .FirstOrDefaultAsync();

        return game;
    }

    public async Task<GameModel?> CreateGame(GameModel game)
    {
        GAME entity = new()
        {
            GAME_NAME = game.GameName
        };

        await _context.GAME.AddAsync(entity);
        await _context.SaveChangesAsync();

        GameModel model = new()
        {
            GameId = entity.GAME_ID,
            GameName = entity.GAME_NAME
        };

        return model;
    }

    public async Task<GameModel?> UpdateGame(GameModel game)
    {
        throw new NotImplementedException();
    }

    public Task DeleteGame(int gameId)
    {
        throw new NotImplementedException();
    }
}
