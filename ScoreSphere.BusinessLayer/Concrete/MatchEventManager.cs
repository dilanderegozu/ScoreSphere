using ScoreSphere.BusinessLayer.Abstract;
using ScoreSphere.DataAccessLayer.Abstract;
using ScoreSphere.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.BusinessLayer.Concrete
{
    public class MatchEventManager : GenericManager<MatchEvent>, IMatchEventService
    {
        public MatchEventManager(IGenericDal<MatchEvent> repository) : base(repository)
        {
        }
    }
}
