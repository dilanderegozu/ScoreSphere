namespace ScoreSphere.DtoLayer.TeamDtos
{
    public class UpdateTeamDto
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string LogoUrl { get; set; }
        public string Stadium { get; set; }
        public string City { get; set; }
    }
}