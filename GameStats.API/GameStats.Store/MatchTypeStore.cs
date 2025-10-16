using GameStats.Data.Context;
using GameStats.Data.Entities;
using GameStats.Model;
using GameStats.Store.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace GameStats.Store;

public class MatchTypeStore(GameStatsDbContext _context) : IMatchTypeStore
{
    public async Task<IEnumerable<MatchTypeModel>> GetMatchTypes(PagedQuery<MatchTypeModel> pagedQuery)
    {
        IQueryable<MATCH_TYPE> query = _context.MATCH_TYPE.AsQueryable();

        if (pagedQuery.Filter != null)
        {
            if (!string.IsNullOrWhiteSpace(pagedQuery.Filter.MatchTypeName))
            {
                query = query.Where(g => g.MATCH_TYPE_NAME.ToLower().Contains(pagedQuery.Filter.MatchTypeName.ToLower()));
            }

            if (pagedQuery.Filter.MatchTypeId > 0)
            {
                query = query.Where(g => g.MATCH_TYPE_ID == pagedQuery.Filter.MatchTypeId);
            }

            if (pagedQuery.Filter.GameId > 0)
            {
                query = query.Where(g => g.GAME_ID == pagedQuery.Filter.GameId);
            }
        }

        query = query.OrderBy(m => m.MATCH_TYPE_ID).ApplyPaging(pagedQuery);

        List<MatchTypeModel> games = await query.Select(m => new MatchTypeModel
        {
            MatchTypeId = m.MATCH_TYPE_ID,
            GameId = m.GAME_ID,
            MatchTypeName = m.MATCH_TYPE_NAME
        }).ToListAsync();

        return games;
    }

    public async Task<MatchTypeModel?> GetMatchType(int matchTypeId)
    {
        return await _context.MATCH_TYPE
            .AsNoTracking()
            .Where(m => m.MATCH_TYPE_ID == matchTypeId)
            .Select(m => new MatchTypeModel
            {
                MatchTypeId = m.MATCH_TYPE_ID,
                GameId = m.GAME_ID,
                MatchTypeName = m.MATCH_TYPE_NAME
            })
            .FirstOrDefaultAsync();
    }

    public async Task<MatchTypeModel?> CreateMatchType(MatchTypeModel matchType)
    {
        var gameExists = await _context.GAME.AnyAsync(g => g.GAME_ID == matchType.GameId);
        if (!gameExists)
        {
            throw new InvalidOperationException($"Game with ID {matchType.GameId} does not exist.");
        }

        MATCH_TYPE entity = new()
        {
            GAME_ID = matchType.GameId,
            MATCH_TYPE_NAME = matchType.MatchTypeName
        };

        await _context.MATCH_TYPE.AddAsync(entity);
        await _context.SaveChangesAsync();

        return new MatchTypeModel
        {
            MatchTypeId = entity.MATCH_TYPE_ID,
            GameId = entity.GAME_ID,
            MatchTypeName = entity.MATCH_TYPE_NAME
        };
    }

    public async Task<MatchTypeModel?> UpdateMatchType(MatchTypeModel matchType)
    {
        var gameExists = await _context.GAME.AnyAsync(g => g.GAME_ID == matchType.GameId);
        if (!gameExists)
        {
            throw new InvalidOperationException($"Game with ID {matchType.GameId} does not exist.");
        }

        MATCH_TYPE? entity = await _context.MATCH_TYPE
            .Where(m => m.MATCH_TYPE_ID == matchType.MatchTypeId)
            .FirstOrDefaultAsync();

        if (entity != null)
        {
            entity.MATCH_TYPE_NAME = matchType.MatchTypeName;
            entity.GAME_ID = matchType.GameId;
            _context.MATCH_TYPE.Update(entity);
            await _context.SaveChangesAsync();

            return new MatchTypeModel
            {
                MatchTypeId = entity.MATCH_TYPE_ID,
                MatchTypeName = entity.MATCH_TYPE_NAME,
                GameId = entity.GAME_ID
            };
        }

        return null;
    }

    public async Task<bool> DeleteMatchType(int matchTypeId)
    {
        MATCH_TYPE? entity = await _context.MATCH_TYPE.FirstOrDefaultAsync(m => m.MATCH_TYPE_ID == matchTypeId);

        if (entity != null)
        {
            _context.MATCH_TYPE.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}
