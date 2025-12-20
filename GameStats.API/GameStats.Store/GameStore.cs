using GameStats.Data.Context;
using GameStats.Data.Entities;
using GameStats.Model;
using GameStats.Store.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameStats.Store;

public class GameStore(GameStatsDbContext _context) : IGameStore
{
    public async Task<DataModel<GameModel>> GetGames(PagedQuery<GameModel> pagedQuery)
    {
        IQueryable<GAME> query = _context.GAME.AsQueryable();
        int count = query.Count();

        if (pagedQuery.Filter != null)
        {
            if (!string.IsNullOrWhiteSpace(pagedQuery.Filter.GameName))
            {
                query = query.Where(g => g.GAME_NAME.ToLower().Contains(pagedQuery.Filter.GameName.ToLower()));
            }

            if (pagedQuery.Filter.GameId > 0)
            {
                query = query.Where(g => g.GAME_ID == pagedQuery.Filter.GameId);
            }
        }

        query = query.OrderBy(m => m.GAME_ID).ApplyPaging(pagedQuery);

        List<GameModel> games = await query.Select(g => new GameModel
        {
            GameId = g.GAME_ID,
            GameName = g.GAME_NAME
        }).ToListAsync();

        return new DataModel<GameModel>
        {
            Data = games ?? [],
            Count = count
        };
    }

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
        GAME? entity = await _context.GAME
            .Where(g => g.GAME_ID == game.GameId)
            .FirstOrDefaultAsync();

        GameModel? model = null;

        if(entity != null)
        {
            entity.GAME_NAME = game.GameName;
            _context.GAME.Update(entity);
            await _context.SaveChangesAsync();
            model = new GameModel
            {
                GameId = entity.GAME_ID,
                GameName = entity.GAME_NAME
            };
        }

        return model;
    }

    public async Task<bool> DeleteGame(int gameId)
    {
        GAME? entity = await _context.GAME
            .Where(g => g.GAME_ID == gameId)
            .FirstOrDefaultAsync();

        if (entity != null)
        {
            _context.GAME.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}
