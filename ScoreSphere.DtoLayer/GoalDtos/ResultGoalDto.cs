namespace ScoreSphere.DtoLayer.GoalDtos
{
    public class ResultGoalDto
    {
        public int GoalId { get; set; }
        public int MatchId { get; set; }

        public int TeamId { get; set; }
        public string TeamName { get; set; }

        public string PlayerName { get; set; }
        public int Minute { get; set; }
        public bool IsOwnGoal { get; set; }
        public bool IsPenalty { get; set; }
    }
}