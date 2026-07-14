using Microsoft.EntityFrameworkCore;
using ScoreSphere.DataAccessLayer.Abstract;
using ScoreSphere.DataAccessLayer.Concrete;
using ScoreSphere.DataAccessLayer.RepositoryDesignPattern;
using ScoreSphere.EntityLayer.Entities;

namespace ScoreSphere.DataAccessLayer.EntityFramework
{
    public class EfMatchDal : GenericRepository<Match>, IMatchDal
    {
        private readonly ApiContext _context;

        public EfMatchDal(ApiContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Match>> GetMatchesByWeekAsync(int seasonId, int week)
        {
            return await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.League)
                .Include(m => m.Season)
                .Where(m => m.SeasonId == seasonId && m.Week == week)
                .ToListAsync();
        }

        public async Task<List<Match>> GetMatchesByStatusAsync(MatchStatus status)
        {
            return await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.League)
                .Include(m => m.Season)
                .Where(m => m.Status == status)
                .ToListAsync();
        }

        public async Task<Match?> GetMatchWithDetailsByIdAsync(int matchId)
        {
            return await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.League)
                .Include(m => m.Season)
                .Include(m => m.MatchStat)
                .Include(m => m.Goals)
                    .ThenInclude(g => g.Team)
                .Include(m => m.MatchEvents)
                .FirstOrDefaultAsync(m => m.MatchId == matchId);
        }
    }
}