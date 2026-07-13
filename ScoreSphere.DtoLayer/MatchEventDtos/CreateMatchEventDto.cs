using ScoreSphere.DtoLayer.Enums;

namespace ScoreSphere.DtoLayer.MatchEventDtos
{
    public class CreateMatchEventDto
    {
        public int MatchId { get; set; }
        public EventType EventType { get; set; }
        public string PlayerName { get; set; }
        public int Minute { get; set; }
        public string Description { get; set; }
    }
}