using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.EntityLayer.Entities
{
    public class MatchEvent
    {
        public int MatchEventId { get; set; }

        public int MatchId { get; set; }

        public Match Match { get; set; }

        public EventType EventType { get; set; }

        public string PlayerName { get; set; }

        public int Minute { get; set; }

        public string Description { get; set; }
    }
    public enum EventType
    {
        Goal,
        YellowCard,
        RedCard,
        Substitution,
        Penalty,
        VAR
    }
}
