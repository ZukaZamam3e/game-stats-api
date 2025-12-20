using GameStats.Data.Context;
using GameStats.Data.Entities;
using GameStats.Model;
using GameStats.Store.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace GameStats.Store;

public class MapStore(GameStatsDbContext _context) : IMapStore
{
    public async Task<DataModel<MapModel>> GetMaps(PagedQuery<MapModel> pagedQuery)
    {
        IQueryable<MAP> query = _context.MAP.AsQueryable();
        int count = query.Count();

        if (pagedQuery.Filter != null)
        {
            if (!string.IsNullOrWhiteSpace(pagedQuery.Filter.MapName))
            {
                query = query.Where(g => g.MAP_NAME.ToLower().Contains(pagedQuery.Filter.MapName.ToLower()));
            }

            if (pagedQuery.Filter.MapId > 0)
            {
                query = query.Where(g => g.MAP_ID == pagedQuery.Filter.MapId);
            }

            if (pagedQuery.Filter.GameId > 0)
            {
                query = query.Where(g => g.GAME_ID == pagedQuery.Filter.GameId);
            }
        }

        query = query.OrderBy(m => m.MAP_ID).ApplyPaging(pagedQuery);

        List<MapModel> maps = await query.Select(g => new MapModel
        {
            MapId = g.MAP_ID,
            GameId = g.GAME_ID,
            MapName = g.MAP_NAME
        }).ToListAsync();

        return new DataModel<MapModel>
        {
            Data = maps ?? [],
            Count = count
        };
    }

    public async Task<MapModel?> GetMap(int mapId)
    {
        return await _context.MAP
            .AsNoTracking()
            .Where(m => m.MAP_ID == mapId)
            .Select(m => new MapModel
            {
                MapId = m.MAP_ID,
                MapName = m.MAP_NAME,
                GameId = m.GAME_ID
            })
            .FirstOrDefaultAsync();
    }

    public async Task<MapModel?> CreateMap(MapModel map)
    {
        var gameExists = await _context.GAME.AnyAsync(g => g.GAME_ID == map.GameId);
        if (!gameExists)
        {
            throw new InvalidOperationException($"Game with ID {map.GameId} does not exist.");
        }

        MAP entity = new()
        {
            MAP_NAME = map.MapName,
            GAME_ID = map.GameId
        };

        await _context.MAP.AddAsync(entity);
        await _context.SaveChangesAsync();

        return new MapModel
        {
            MapId = entity.MAP_ID,
            MapName = entity.MAP_NAME,
            GameId = entity.GAME_ID
        };
    }

    public async Task<MapModel?> UpdateMap(MapModel map)
    {
        var gameExists = await _context.GAME.AnyAsync(g => g.GAME_ID == map.GameId);
        if (!gameExists)
        {
            throw new InvalidOperationException($"Game with ID {map.GameId} does not exist.");
        }

        MAP? entity = await _context.MAP
            .Where(m => m.MAP_ID == map.MapId)
            .FirstOrDefaultAsync();

        if (entity != null)
        {
            entity.MAP_NAME = map.MapName;
            entity.GAME_ID = map.GameId;
            _context.MAP.Update(entity);
            await _context.SaveChangesAsync();

            return new MapModel
            {
                MapId = entity.MAP_ID,
                MapName = entity.MAP_NAME,
                GameId = entity.GAME_ID
            };
        }

        return null;
    }

    public async Task<bool> DeleteMap(int mapId)
    {
        MAP? entity = await _context.MAP.FirstOrDefaultAsync(m => m.MAP_ID == mapId);

        if (entity != null)
        {
            _context.MAP.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}