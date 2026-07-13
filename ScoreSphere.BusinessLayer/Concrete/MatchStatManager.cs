using ScoreSphere.BusinessLayer.Abstract;
using ScoreSphere.DataAccessLayer.Abstract;
using ScoreSphere.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.BusinessLayer.Concrete
{
    public class MatchStatManager : GenericManager<MatchStat>, IMatchStatService
    {
        public MatchStatManager(IGenericDal<MatchStat> repository) : base(repository)
        {
        }
    }
}
