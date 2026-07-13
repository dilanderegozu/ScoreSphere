using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.EntityLayer.Entities
{
    public class Goal
    {
        public int GoalId { get; set; }

        public int MatchId { get; set; }

        public Match Match { get; set; }

        public int TeamId { get; set; }

        public Team Team { get; set; }

        public string PlayerName { get; set; }

        public int Minute { get; set; }

        public bool IsOwnGoal { get; set; }

        public bool IsPenalty { get; set; }
    }
}
