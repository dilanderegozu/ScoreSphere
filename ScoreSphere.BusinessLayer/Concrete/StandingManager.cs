using ScoreSphere.BusinessLayer.Abstract;
using ScoreSphere.DataAccessLayer.Abstract;
using ScoreSphere.DtoLayer.StandingDtos;
using ScoreSphere.EntityLayer.Entities;

namespace ScoreSphere.BusinessLayer.Concrete
{
    public class StandingManager : IStandingService
    {
        private readonly IMatchDal _matchDal;

        public StandingManager(IMatchDal matchDal)
        {
            _matchDal = matchDal;
        }

        public async Task<List<ResultStandingDto>> GetStandingsBySeasonAsync(int seasonId)
        {
            var matches = await _matchDal.GetListAsync(
                m => m.HomeTeam!, m => m.AwayTeam!
            );

            var finishedMatches = matches
                .Where(m => m.SeasonId == seasonId
                            && m.Status == MatchStatus.Finished
                            && m.HomeScore.HasValue
                            && m.AwayScore.HasValue)
                .OrderBy(m => m.MatchDate)
                .ToList();

            var teamStats = new Dictionary<int, ResultStandingDto>();

            void EnsureTeam(int teamId, string teamName, string logoUrl)
            {
                if (!teamStats.ContainsKey(teamId))
                {
                    teamStats[teamId] = new ResultStandingDto
                    {
                        TeamId = teamId,
                        TeamName = teamName,
                        LogoUrl = logoUrl
                    };
                }
            }

            foreach (var match in finishedMatches)
            {
                EnsureTeam(match.HomeTeamId, match.HomeTeam!.TeamName, match.HomeTeam.LogoUrl);
                EnsureTeam(match.AwayTeamId, match.AwayTeam!.TeamName, match.AwayTeam.LogoUrl);

                var home = teamStats[match.HomeTeamId];
                var away = teamStats[match.AwayTeamId];

                home.Played++;
                away.Played++;
                home.GoalsFor += match.HomeScore!.Value;
                home.GoalsAgainst += match.AwayScore!.Value;
                away.GoalsFor += match.AwayScore!.Value;
                away.GoalsAgainst += match.HomeScore!.Value;

                if (match.HomeScore > match.AwayScore)
                {
                    home.Won++; away.Lost++;
                    home.Points += 3;
                    home.Form.Add("W"); away.Form.Add("L");
                }
                else if (match.HomeScore < match.AwayScore)
                {
                    away.Won++; home.Lost++;
                    away.Points += 3;
                    home.Form.Add("L"); away.Form.Add("W");
                }
                else
                {
                    home.Drawn++; away.Drawn++;
                    home.Points++; away.Points++;
                    home.Form.Add("D"); away.Form.Add("D");
                }
            }

            foreach (var stat in teamStats.Values)
            {
                stat.GoalDifference = stat.GoalsFor - stat.GoalsAgainst;
                stat.Form = stat.Form.TakeLast(5).ToList();
            }

            return teamStats.Values
                .OrderByDescending(s => s.Points)
                .ThenByDescending(s => s.GoalDifference)
                .ThenByDescending(s => s.GoalsFor)
                .ToList();
        }
    }
}