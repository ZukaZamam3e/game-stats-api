using GameStats.Data.Context;
using GameStats.Data.Entities;
using GameStats.Model;
using GameStats.Store.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameStats.Store;

public class PlayerStore(GameStatsDbContext _context) : IPlayerStore
{
    public async Task<IEnumerable<PlayerModel>> GetPlayers(PagedQuery<PlayerModel> pagedQuery)
    {
        IQueryable<PLAYER> query = _context.PLAYER.AsQueryable();

        if (pagedQuery.Filter != null)
        {
            if (!string.IsNullOrWhiteSpace(pagedQuery.Filter.PlayerName))
            {
                query = query.Where(m => m.PLAYER_NAME.ToLower().Contains(pagedQuery.Filter.PlayerName.ToLower()));
            }

            if (pagedQuery.Filter.GameId > 0)
            {
                query = query.Where(m => m.GAME_ID == pagedQuery.Filter.GameId);
            }

            if (pagedQuery.Filter.PlayerId > 0)
            {
                query = query.Where(m => m.PLAYER_ID == pagedQuery.Filter.PlayerId);
            }
        }

        query = query.OrderBy(m => m.PLAYER_ID).ApplyPaging(pagedQuery);

        List<PlayerModel> players = await query.Select(g => new PlayerModel
        {
            PlayerId = g.PLAYER_ID,
            PlayerName = g.PLAYER_NAME,
            GameId = g.GAME_ID,
            Emblem = g.EMBLEM
        }).ToListAsync();

        return players;
    }

    public async Task<PlayerModel?> GetPlayer(int playerId)
    {
        return await _context.PLAYER
            .AsNoTracking()
            .Where(m => m.PLAYER_ID == playerId)
            .Select(m => new PlayerModel
            {
                PlayerId = m.PLAYER_ID,
                PlayerName = m.PLAYER_NAME,
                GameId = m.GAME_ID,
                Emblem = m.EMBLEM
            })
            .FirstOrDefaultAsync();
    }

    public async Task<PlayerModel?> CreatePlayer(PlayerModel player)
    {
        var gameExists = await _context.GAME.AnyAsync(g => g.GAME_ID == player.GameId);
        if (!gameExists)
        {
            throw new InvalidOperationException($"Game with ID {player.GameId} does not exist.");
        }

        PLAYER entity = new()
        {
            PLAYER_NAME = player.PlayerName,
            GAME_ID = player.GameId,
            EMBLEM = player.Emblem
        };

        await _context.PLAYER.AddAsync(entity);
        await _context.SaveChangesAsync();

        player.PlayerId = entity.PLAYER_ID;
        return player;
    }

    public async Task<PlayerModel?> UpdatePlayer(PlayerModel player)
    {
        var gameExists = await _context.GAME.AnyAsync(g => g.GAME_ID == player.GameId);
        if (!gameExists)
        {
            throw new InvalidOperationException($"Game with ID {player.GameId} does not exist.");
        }

        PLAYER? entity = await _context.PLAYER.FirstOrDefaultAsync(m => m.PLAYER_ID == player.PlayerId);

        if (entity != null)
        {
            entity.PLAYER_NAME = player.PlayerName;
            entity.GAME_ID = player.GameId;
            entity.EMBLEM = player.Emblem;
            await _context.SaveChangesAsync();
            return player;
        }
        else
        {
            return null;
        }
    }

    public async Task<bool> DeletePlayer(int playerId)
    {
        PLAYER? entity = await _context.PLAYER.FirstOrDefaultAsync(m => m.PLAYER_ID == playerId);

        if (entity != null)
        {
            _context.PLAYER.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }
    }
}
