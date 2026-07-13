using ScoreSphere.DataAccessLayer.Abstract;
using ScoreSphere.DataAccessLayer.Concrete;
using ScoreSphere.DataAccessLayer.RepositoryDesignPattern;
using ScoreSphere.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.DataAccessLayer.EntityFramework
{
    public class EfTeamDal : GenericRepository<Team>, ITeamDal
    {
        public EfTeamDal(ApiContext context) : base(context)
        {
        }
    }
}
