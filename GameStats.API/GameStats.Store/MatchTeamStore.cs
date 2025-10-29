using GameStats.Data.Context;
using GameStats.Data.Entities;
using GameStats.Model;
using GameStats.Store.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameStats.Store;

public class MatchTeamStore(GameStatsDbContext _context) : IMatchTeamStore
{
    public async Task<IEnumerable<MatchTeamModel>> GetMatcheTeams(PagedQuery<MatchTeamModel> pagedQuery)
    {
        IQueryable<MATCH_TEAM> query = _context.MATCH_TEAM.AsQueryable();

        if (pagedQuery.Filter != null)
        {
            if (pagedQuery.Filter.MatchTeamId > 0)
            {
                query = query.Where(mt => mt.MATCH_TEAM_ID == pagedQuery.Filter.MatchTeamId);
            }

            if (pagedQuery.Filter.MatchId > 0)
            {
                query = query.Where(mt => mt.MATCH_ID == pagedQuery.Filter.MatchId);
            }

            if (!string.IsNullOrWhiteSpace(pagedQuery.Filter.TeamColor))
            {
                string color = pagedQuery.Filter.TeamColor.ToLower();
                query = query.Where(mt => mt.TEAM_COLOR.ToLower().Contains(color));
            }
        }

        query = query.OrderBy(mt => mt.MATCH_TEAM_ID).ApplyPaging(pagedQuery);

        List<MatchTeamModel> teams = await query.Select(mt => new MatchTeamModel
        {
            MatchTeamId = mt.MATCH_TEAM_ID,
            MatchId = mt.MATCH_ID,
            TeamColor = mt.TEAM_COLOR
        }).ToListAsync();

        return teams;
    }

    public async Task<MatchTeamModel?> GetMatchTeam(int matchTeamId)
    {
        return await _context.MATCH_TEAM
            .AsNoTracking()
            .Where(mt => mt.MATCH_TEAM_ID == matchTeamId)
            .Select(mt => new MatchTeamModel
            {
                MatchTeamId = mt.MATCH_TEAM_ID,
                MatchId = mt.MATCH_ID,
                TeamColor = mt.TEAM_COLOR
            })
            .FirstOrDefaultAsync();
    }

    public async Task<MatchTeamModel?> CreateMatchTeam(MatchTeamModel matchTeam)
    {
        var matchExists = await _context.MATCH.AnyAsync(m => m.MATCH_ID == matchTeam.MatchId);
        if (!matchExists)
        {
            throw new InvalidOperationException($"Match with ID {matchTeam.MatchId} does not exist.");
        }

        MATCH_TEAM entity = new()
        {
            MATCH_ID = matchTeam.MatchId,
            TEAM_COLOR = matchTeam.TeamColor
        };

        await _context.MATCH_TEAM.AddAsync(entity);
        await _context.SaveChangesAsync();

        return new MatchTeamModel
        {
            MatchTeamId = entity.MATCH_TEAM_ID,
            MatchId = entity.MATCH_ID,
            TeamColor = entity.TEAM_COLOR
        };
    }

    public async Task<MatchTeamModel?> UpdateMatchTeam(MatchTeamModel matchTeam)
    {
        var matchExists = await _context.MATCH.AnyAsync(m => m.MATCH_ID == matchTeam.MatchId);
        if (!matchExists)
        {
            throw new InvalidOperationException($"Match with ID {matchTeam.MatchId} does not exist.");
        }

        MATCH_TEAM? entity = await _context.MATCH_TEAM
            .Where(mt => mt.MATCH_TEAM_ID == matchTeam.MatchTeamId)
            .FirstOrDefaultAsync();

        if (entity != null)
        {
            entity.MATCH_ID = matchTeam.MatchId;
            entity.TEAM_COLOR = matchTeam.TeamColor;
            _context.MATCH_TEAM.Update(entity);
            await _context.SaveChangesAsync();

            return new MatchTeamModel
            {
                MatchTeamId = entity.MATCH_TEAM_ID,
                MatchId = entity.MATCH_ID,
                TeamColor = entity.TEAM_COLOR
            };
        }

        return null;
    }

    public async Task<bool> DeleteMatchTeam(int matchTeamId)
    {
        MATCH_TEAM? entity = await _context.MATCH_TEAM.FirstOrDefaultAsync(mt => mt.MATCH_TEAM_ID == matchTeamId);

        if (entity != null)
        {
            _context.MATCH_TEAM.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}
