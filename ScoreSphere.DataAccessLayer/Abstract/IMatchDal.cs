using ScoreSphere.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.DataAccessLayer.Abstract
{
    public interface IMatchDal:IGenericDal<Match>
    {
        Task<List<Match>> GetMatchesByWeekAsync(int seasonId, int week);
        Task<List<Match>> GetMatchesByStatusAsync(MatchStatus status);
        Task<Match?> GetMatchWithDetailsByIdAsync(int id);
    }
}
