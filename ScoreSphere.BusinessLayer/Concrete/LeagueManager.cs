using ScoreSphere.BusinessLayer.Abstract;
using ScoreSphere.DataAccessLayer.Abstract;
using ScoreSphere.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.BusinessLayer.Concrete
{
    public class LeagueManager : GenericManager<League>, ILeagueService
    {
        public LeagueManager(IGenericDal<League> repository) : base(repository)
        {
        }
    }
}
