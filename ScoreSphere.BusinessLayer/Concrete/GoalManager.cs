using ScoreSphere.BusinessLayer.Abstract;
using ScoreSphere.DataAccessLayer.Abstract;
using ScoreSphere.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.BusinessLayer.Concrete
{
    public class GoalManager : GenericManager<Goal>, IGoalService
    {
        public GoalManager(IGenericDal<Goal> repository) : base(repository)
        {
        }
    }
}
