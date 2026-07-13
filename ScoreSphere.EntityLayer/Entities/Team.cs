using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.EntityLayer.Entities
{
    public class Team
    {
        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public string LogoUrl { get; set; }

        public string Stadium { get; set; }

        public string City { get; set; }

        public ICollection<Match> HomeMatches { get; set; }

        public ICollection<Match> AwayMatches { get; set; }
    }
}
