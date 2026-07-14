using ScoreSphere.DtoLayer.MatchDtos;

namespace ScoreSphere.BusinessLayer.Abstract
{
    public interface IMatchService
    {
        Task<List<ResultMatchDto>> TGetListAsync();
        Task<ResultMatchDto?> TGetByIdAsync(int id);
        Task TInsertAsync(CreateMatchDto dto);
        Task TUpdateAsync(UpdateMatchDto dto);
        Task TDeleteAsync(int id);

        Task<List<ResultMatchDto>> GetMatchesByWeekAsync(int seasonId, int week);
        Task<List<ResultMatchDto>> GetMatchesByStatusAsync(string status);
    }
}