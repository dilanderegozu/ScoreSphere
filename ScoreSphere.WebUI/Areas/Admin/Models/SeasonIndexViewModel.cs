namespace ScoreSphere.WebUI.Areas.Admin.Models
{
    public class SeasonIndexViewModel
    {
        public int ActiveSeasons { get; set; }
        public int UpcomingSeasons { get; set; }
        public int CompletedSeasons { get; set; }
        public List<SeasonListItemViewModel> Seasons { get; set; } = new();
    }

    public class SeasonListItemViewModel
    {
        public int SeasonId { get; set; }
        public string SeasonName { get; set; }
        public string LeagueName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
    }
}