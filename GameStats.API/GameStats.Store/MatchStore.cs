using GameStats.Data.Context;
using GameStats.Data.Entities;
using GameStats.Model;
using GameStats.Store.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameStats.Store;

public class MatchStore(GameStatsDbContext _context) : IMatchStore
{
    public async Task<DataModel<MatchModel>> GetMatches(PagedQuery<MatchModel> pagedQuery)
    {
        IQueryable<MATCH> query = _context.MATCH.AsQueryable();
        int count = query.Count();

        if (pagedQuery.Filter != null)
        {
            if (pagedQuery.Filter.MatchId > 0)
            {
                query = query.Where(g => g.MATCH_ID == pagedQuery.Filter.MatchId);
            }

            if (pagedQuery.Filter.MatchId > 0)
            {
                query = query.Where(g => g.OLD_MATCH_ID == pagedQuery.Filter.OldMatchId);
            }

            if (pagedQuery.Filter.GameId > 0)
            {
                query = query.Where(g => g.GAME_ID == pagedQuery.Filter.GameId);
            }

            if (!string.IsNullOrWhiteSpace(pagedQuery.Filter.MatchName))
            {
                query = query.Where(g => g.MATCH_NAME.ToLower().Contains(pagedQuery.Filter.MatchName.ToLower()));
            }

            if (pagedQuery.Filter.TypeCd > 0)
            {
                query = query.Where(g => g.TYPE_CD == pagedQuery.Filter.TypeCd);
            }

            if (pagedQuery.Filter.MapId > 0)
            {
                query = query.Where(g => g.MAP_ID == pagedQuery.Filter.MapId);
            }
        }

        query = query.OrderBy(m => m.MATCH_ID).ApplyPaging(pagedQuery);

        List<MatchModel> matches = await query.Select(m => new MatchModel
        {
            MatchId = m.MATCH_ID,
            OldMatchId = m.OLD_MATCH_ID,
            GameId = m.GAME_ID,
            MatchName = m.MATCH_NAME,
            TypeCd = m.TYPE_CD,
            MapId = m.MAP_ID,
            TotalTime = m.TOTAL_TIME,
            CreatedDate = m.CREATED_DATE
        }).ToListAsync();

        return new DataModel<MatchModel>
        {
            Data = matches ?? [],
            Count = count
        };
    }

    public async Task<MatchModel?> GetMatch(int matchId)
    {
        return await _context.MATCH
            .AsNoTracking()
            .Where(m => m.MATCH_ID == matchId)
            .Select(m => new MatchModel
            {
                MatchId = m.MATCH_ID,
                OldMatchId = m.OLD_MATCH_ID,
                GameId = m.GAME_ID,
                MatchName = m.MATCH_NAME,
                TypeCd = m.TYPE_CD,
                MapId = m.MAP_ID,
                TotalTime = m.TOTAL_TIME,
                CreatedDate = m.CREATED_DATE
            })
            .FirstOrDefaultAsync();
    }

    public async Task<MatchModel?> CreateMatch(MatchModel match)
    {
        var gameExists = await _context.GAME.AnyAsync(g => g.GAME_ID == match.GameId);
        if (!gameExists)
        {
            throw new InvalidOperationException($"Game with ID {match.GameId} does not exist.");
        }

        var matchTypeExists = await _context.MATCH_TYPE.AnyAsync(g => g.MATCH_TYPE_ID == match.TypeCd);
        if (!matchTypeExists)
        {
            throw new InvalidOperationException($"Match Type with ID {match.TypeCd} does not exist.");
        }

        var mapExists = await _context.MAP.AnyAsync(g => g.MAP_ID == match.MapId);
        if (!mapExists)
        {
            throw new InvalidOperationException($"Map with ID {match.MapId} does not exist.");
        }

        MATCH entity = new MATCH()
        {
            OLD_MATCH_ID = match.OldMatchId,
            GAME_ID = match.GameId, 
            MATCH_NAME = match.MatchName,
            TYPE_CD = match.TypeCd,
            MAP_ID = match.MapId,
            TOTAL_TIME = match.TotalTime,
            CREATED_DATE = match.CreatedDate
        };

        await _context.MATCH.AddAsync(entity);
        await _context.SaveChangesAsync();

        return new MatchModel
        {
            MatchId = entity.MATCH_ID,
            OldMatchId = entity.OLD_MATCH_ID,
            GameId = entity.GAME_ID,
            MatchName = entity.MATCH_NAME,
            TypeCd = entity.TYPE_CD,
            MapId = entity.MAP_ID,
            TotalTime = entity.TOTAL_TIME,
            CreatedDate = entity.CREATED_DATE
        };
    }

    public async Task<MatchModel?> UpdateMatch(MatchModel match)
    {
        var gameExists = await _context.GAME.AnyAsync(g => g.GAME_ID == match.GameId);
        if (!gameExists)
        {
            throw new InvalidOperationException($"Game with ID {match.GameId} does not exist.");
        }

        var matchTypeExists = await _context.MATCH_TYPE.AnyAsync(g => g.MATCH_TYPE_ID == match.TypeCd);
        if (!matchTypeExists)
        {
            throw new InvalidOperationException($"Match Type with ID {match.TypeCd} does not exist.");
        }

        var mapExists = await _context.MAP.AnyAsync(g => g.MAP_ID == match.MapId);
        if (!mapExists)
        {
            throw new InvalidOperationException($"Map with ID {match.MapId} does not exist.");
        }

        MATCH? entity = await _context.MATCH
            .Where(m => m.MATCH_ID == match.MatchId)
            .FirstOrDefaultAsync();

        if (entity != null)
        {
            entity.OLD_MATCH_ID = match.OldMatchId;
            entity.GAME_ID = match.GameId;
            entity.MATCH_NAME = match.MatchName;
            entity.TYPE_CD = match.TypeCd;
            entity.MAP_ID = match.MapId;
            entity.TOTAL_TIME = match.TotalTime;
            entity.CREATED_DATE = match.CreatedDate;

            _context.MATCH.Update(entity);
            await _context.SaveChangesAsync();

            return new MatchModel
            {
                MatchId = entity.MATCH_ID,
                OldMatchId = entity.OLD_MATCH_ID,
                GameId = entity.GAME_ID,
                MatchName = entity.MATCH_NAME,
                TypeCd = entity.TYPE_CD,
                MapId = entity.MAP_ID,
                TotalTime = entity.TOTAL_TIME,
                CreatedDate = entity.CREATED_DATE
            };
        }

        return null;

    }

    public async Task<bool> DeleteMatch(int matchId)
    {
        MATCH? entity = await _context.MATCH.FirstOrDefaultAsync(m => m.MATCH_ID == matchId);

        if (entity != null)
        {
            _context.MATCH.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;

    }
}
