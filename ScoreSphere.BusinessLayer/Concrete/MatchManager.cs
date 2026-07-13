using ScoreSphere.BusinessLayer.Abstract;
using ScoreSphere.DataAccessLayer.Abstract;

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ScoreSphere.BusinessLayer.Concrete
{
    public class MatchManager : GenericManager<Match>, IMatchService
    {
        public MatchManager(IGenericDal<Match> repository) : base(repository)
        {
        }
    }
}
