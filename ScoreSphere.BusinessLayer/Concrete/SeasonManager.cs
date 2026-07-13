using ScoreSphere.BusinessLayer.Abstract;
using ScoreSphere.DataAccessLayer.Abstract;
using ScoreSphere.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.BusinessLayer.Concrete
{
    public class SeasonManager : GenericManager<Season>, ISeasonService
    {
        public SeasonManager(IGenericDal<Season> repository) : base(repository)
        {
        }
    }
}
