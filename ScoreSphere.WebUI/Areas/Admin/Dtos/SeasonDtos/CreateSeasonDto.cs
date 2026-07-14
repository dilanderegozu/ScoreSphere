namespace ScoreSphere.WebUI.Areas.Admin.Dtos.SeasonDtos
{
    public class CreateSeasonDto
    {
        public string SeasonName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        public int LeagueId { get; set; }
    }
}
