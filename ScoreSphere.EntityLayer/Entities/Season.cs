using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.EntityLayer.Entities
{
    public class Season
    {
        public int SeasonId { get; set; }
        public string SeasonName { get; set; }   // Örn: "2025-2026"
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }        // Şu an aktif sezon mu?

        public int LeagueId { get; set; }
        public League? League { get; set; }

        public ICollection<Match>? Matches { get; set; }
    }
}
