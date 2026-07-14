using AutoMapper;
using ScoreSphere.BusinessLayer.Abstract;
using ScoreSphere.DataAccessLayer.Abstract;
using ScoreSphere.DtoLayer.TeamDtos;
using ScoreSphere.EntityLayer.Entities;

namespace ScoreSphere.BusinessLayer.Concrete
{
    public class TeamManager : ITeamService
    {
        private readonly ITeamDal _teamDal;
        private readonly IMapper _mapper;

        public TeamManager(ITeamDal teamDal, IMapper mapper)
        {
            _teamDal = teamDal;
            _mapper = mapper;
        }

        public async Task<List<ResultTeamDto>> TGetListAsync()
        {
            var teams = await _teamDal.GetListAsync();
            return _mapper.Map<List<ResultTeamDto>>(teams);
        }

        public async Task<ResultTeamDto?> TGetByIdAsync(int id)
        {
            var team = await _teamDal.GetByIdAsync(id);
            return team == null ? null : _mapper.Map<ResultTeamDto>(team);
        }

        public async Task TInsertAsync(CreateTeamDto dto)
        {
            var team = _mapper.Map<Team>(dto);
            await _teamDal.InsertAsync(team);
        }

        public async Task TUpdateAsync(UpdateTeamDto dto)
        {
            var team = _mapper.Map<Team>(dto);
            await _teamDal.UpdateAsync(team);
        }

        public async Task TDeleteAsync(int id)
        {
            await _teamDal.DeleteAsync(id);
        }
    }
}