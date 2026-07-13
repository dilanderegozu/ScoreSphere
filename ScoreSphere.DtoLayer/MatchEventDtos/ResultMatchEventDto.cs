using ScoreSphere.DtoLayer.Enums;

namespace ScoreSphere.DtoLayer.MatchEventDtos
{
    public class ResultMatchEventDto
    {
        public int MatchEventId { get; set; }
        public int MatchId { get; set; }

        public string EventType { get; set; }
        public string PlayerName { get; set; }
        public int Minute { get; set; }
        public string Description { get; set; }
    }
}