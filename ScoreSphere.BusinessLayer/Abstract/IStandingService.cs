using ScoreSphere.DtoLayer.StandingDtos;

namespace ScoreSphere.BusinessLayer.Abstract
{
    public interface IStandingService
    {
        Task<List<ResultStandingDto>> GetStandingsBySeasonAsync(int seasonId);
    }
}