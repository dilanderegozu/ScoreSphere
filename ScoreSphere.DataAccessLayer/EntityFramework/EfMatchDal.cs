using ScoreSphere.DataAccessLayer.Abstract;
using ScoreSphere.DataAccessLayer.Concrete;
using ScoreSphere.DataAccessLayer.RepositoryDesignPattern;
using ScoreSphere.EntityLayer.Entities;   

namespace ScoreSphere.DataAccessLayer.EntityFramework
{
    public class EfMatchDal : GenericRepository<Match>, IMatchDal
    {
        public EfMatchDal(ApiContext context) : base(context)
        {
        }
    }
}