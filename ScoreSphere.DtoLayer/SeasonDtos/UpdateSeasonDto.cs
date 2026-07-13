namespace ScoreSphere.DtoLayer.SeasonDtos
{
    public class UpdateSeasonDto
    {
        public int SeasonId { get; set; }
        public string SeasonName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        public int LeagueId { get; set; }
    }
}