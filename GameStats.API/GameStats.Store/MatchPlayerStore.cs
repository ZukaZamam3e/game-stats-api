using GameStats.Data.Context;
using GameStats.Data.Entities;
using GameStats.Model;
using GameStats.Store.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace GameStats.Store;

public class MatchPlayerStore(GameStatsDbContext _context) : IMatchPlayerStore
{
    public async Task<IEnumerable<MatchPlayerModel>> GetMatchPlayers(PagedQuery<MatchPlayerModel> pagedQuery)
    {
        IQueryable<MATCH_PLAYER> query = _context.MATCH_PLAYER.AsQueryable();

        if (pagedQuery.Filter != null)
        {
            if (pagedQuery.Filter.MatchPlayerId > 0)
            {
                query = query.Where(g => g.MATCH_PLAYER_ID == pagedQuery.Filter.MatchPlayerId);
            }

            if (pagedQuery.Filter.MatchId > 0)
            {
                query = query.Where(g => g.MATCH_ID == pagedQuery.Filter.MatchId);
            }

            if (pagedQuery.Filter.MatchTeamId > 0)
            {
                query = query.Where(g => g.MATCH_TEAM_ID == pagedQuery.Filter.MatchTeamId);
            }

            if (pagedQuery.Filter.PlayerId > 0)
            {
                query = query.Where(g => g.PLAYER_ID == pagedQuery.Filter.PlayerId);
            }
        }

        query = query.OrderBy(m => m.MATCH_PLAYER_ID).ApplyPaging(pagedQuery);

        List<MatchPlayerModel> matchPlayers = await query.Select(m => new MatchPlayerModel
        {
            MatchPlayerId = m.MATCH_PLAYER_ID,
            MatchId = m.MATCH_ID,
            MatchTeamId = m.MATCH_TEAM_ID,
            PlayerId = m.PLAYER_ID,
            Score = m.SCORE
        }).ToListAsync();

        return matchPlayers;
    }

    public async Task<MatchPlayerModel?> GetMatchPlayer(int matchPlayerId)
    {
        return await _context.MATCH_PLAYER
            .AsNoTracking()
            .Where(m => m.MATCH_PLAYER_ID == matchPlayerId)
            .Select(m => new MatchPlayerModel
            {
                MatchPlayerId = m.MATCH_PLAYER_ID,
                MatchId = m.MATCH_ID,
                MatchTeamId = m.MATCH_TEAM_ID,
                PlayerId = m.PLAYER_ID,
                Score = m.SCORE
            })
            .FirstOrDefaultAsync();
    }

    public async Task<MatchPlayerModel?> CreateMatchPlayer(MatchPlayerModel matchPlayer)
    {
        var matchExists = await _context.MATCH.AnyAsync(g => g.MATCH_ID == matchPlayer.MatchId);
        if (!matchExists)
        {
            throw new InvalidOperationException($"Match with ID {matchPlayer.MatchId} does not exist.");
        }

        var matchTeamExists = await _context.MATCH_TEAM.AnyAsync(g => g.MATCH_TEAM_ID == matchPlayer.MatchTeamId);
        if (!matchTeamExists)
        {
            throw new InvalidOperationException($"Match Team with ID {matchPlayer.MatchTeamId} does not exist.");
        }

        var playerExists = await _context.PLAYER.AnyAsync(g => g.PLAYER_ID == matchPlayer.PlayerId);
        if (!playerExists)
        {
            throw new InvalidOperationException($"Player with ID {matchPlayer.PlayerId} does not exist.");
        }

        MATCH_PLAYER entity = new()
        {
            MATCH_ID = matchPlayer.MatchId,
            MATCH_TEAM_ID = matchPlayer.MatchTeamId,
            PLAYER_ID = matchPlayer.PlayerId,
            SCORE = matchPlayer.Score
        };

        await _context.MATCH_PLAYER.AddAsync(entity);
        await _context.SaveChangesAsync();

        return new MatchPlayerModel
        {
            MatchPlayerId = entity.MATCH_PLAYER_ID,
            MatchId = entity.MATCH_ID,
            MatchTeamId = entity.MATCH_TEAM_ID,
            PlayerId = entity.PLAYER_ID,
            Score = entity.SCORE
        };
    }

    public async Task<MatchPlayerModel?> UpdateMatchPlayer(MatchPlayerModel matchPlayer)
    {
        var matchExists = await _context.MATCH.AnyAsync(g => g.MATCH_ID == matchPlayer.MatchId);
        if (!matchExists)
        {
            throw new InvalidOperationException($"Match with ID {matchPlayer.MatchId} does not exist.");
        }

        var matchTeamExists = await _context.MATCH_TEAM.AnyAsync(g => g.MATCH_TEAM_ID == matchPlayer.MatchTeamId);
        if (!matchTeamExists)
        {
            throw new InvalidOperationException($"Match Team with ID {matchPlayer.MatchTeamId} does not exist.");
        }

        var playerExists = await _context.PLAYER.AnyAsync(g => g.PLAYER_ID == matchPlayer.PlayerId);
        if (!playerExists)
        {
            throw new InvalidOperationException($"Player with ID {matchPlayer.PlayerId} does not exist.");
        }

        MATCH_PLAYER? entity = await _context.MATCH_PLAYER.FirstOrDefaultAsync(m => m.MATCH_PLAYER_ID == matchPlayer.MatchPlayerId);

        if (entity != null)
        {
            entity.MATCH_ID = matchPlayer.MatchId;
            entity.MATCH_TEAM_ID = matchPlayer.MatchTeamId;
            entity.PLAYER_ID = matchPlayer.PlayerId;
            entity.SCORE = matchPlayer.Score;
            _context.MATCH_PLAYER.Update(entity);
            await _context.SaveChangesAsync();

            return new MatchPlayerModel
            {
                MatchPlayerId = entity.MATCH_PLAYER_ID,
                MatchId = entity.MATCH_ID,
                MatchTeamId = entity.MATCH_TEAM_ID,
                PlayerId = entity.PLAYER_ID,
                Score = entity.SCORE
            };
        }

        return null;
    }

    public async Task<bool> DeleteMatchPlayer(int matchPlayerId)
    {
        MATCH_PLAYER? entity = await _context.MATCH_PLAYER.FirstOrDefaultAsync(m => m.MATCH_PLAYER_ID == matchPlayerId);

        if (entity != null)
        {
            _context.MATCH_PLAYER.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}
