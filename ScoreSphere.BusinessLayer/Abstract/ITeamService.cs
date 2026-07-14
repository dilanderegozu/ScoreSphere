using ScoreSphere.DtoLayer.TeamDtos;

namespace ScoreSphere.BusinessLayer.Abstract
{
    public interface ITeamService
    {
        Task<List<ResultTeamDto>> TGetListAsync();
        Task<ResultTeamDto?> TGetByIdAsync(int id);
        Task TInsertAsync(CreateTeamDto dto);
        Task TUpdateAsync(UpdateTeamDto dto);
        Task TDeleteAsync(int id);
    }
}