using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.EntityLayer.Entities
{
    public class League
    {
        public int LeagueId { get; set; }

        public string LeagueName { get; set; }

        public string Country { get; set; }

        public string LogoUrl { get; set; }

        public ICollection<Match> Matches { get; set; }
    }
}
