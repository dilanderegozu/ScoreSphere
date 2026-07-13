using ScoreSphere.DataAccessLayer.Abstract;
using ScoreSphere.DataAccessLayer.Concrete;
using ScoreSphere.DataAccessLayer.RepositoryDesignPattern;
using ScoreSphere.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.DataAccessLayer.EntityFramework
{
    public class EfSeasonDal : GenericRepository<Season>, ISeasonDal
    {
        public EfSeasonDal(ApiContext context) : base(context)
        {
        }
    }
}
