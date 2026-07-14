using AutoMapper;
using ScoreSphere.BusinessLayer.Abstract;
using ScoreSphere.DataAccessLayer.Abstract;
using ScoreSphere.DtoLayer.MatchDtos;
using ScoreSphere.EntityLayer.Entities;

namespace ScoreSphere.BusinessLayer.Concrete
{
    public class MatchManager : GenericManager<Match>, IMatchService
    {
        private readonly IMatchDal _matchDal;
        private readonly IMapper _mapper;

        public MatchManager(IMatchDal matchDal, IMapper mapper) : base(matchDal)
        {
            _matchDal = matchDal;
            _mapper = mapper;
        }

        public async Task<List<ResultMatchDto>> TGetListAsync()
        {
            var matches = await base.TGetListAsync(
                m => m.HomeTeam!, m => m.AwayTeam!, m => m.League!, m => m.Season!
            );
            return _mapper.Map<List<ResultMatchDto>>(matches);
        }

        public async Task<ResultMatchDto?> TGetByIdAsync(int id)
        {
            var match = await base.TGetByIdAsync(id,
                m => m.HomeTeam!, m => m.AwayTeam!, m => m.League!, m => m.Season!
            );
            return match == null ? null : _mapper.Map<ResultMatchDto>(match);
        }

        public async Task TInsertAsync(CreateMatchDto dto)
        {
            var match = _mapper.Map<Match>(dto);
            await base.TInsertAsync(match);
        }

        public async Task TUpdateAsync(UpdateMatchDto dto)
        {
            var match = _mapper.Map<Match>(dto);
            await base.TUpdateAsync(match);
        }

        public async Task TDeleteAsync(int id)
        {
            await base.TDeleteAsync(id);
        }

        public async Task<List<ResultMatchDto>> GetMatchesByWeekAsync(int seasonId, int week)
        {
            var matches = await _matchDal.GetMatchesByWeekAsync(seasonId, week);
            return _mapper.Map<List<ResultMatchDto>>(matches);
        }

        public async Task<List<ResultMatchDto>> GetMatchesByStatusAsync(string status)
        {
            var statusEnum = Enum.Parse<MatchStatus>(status, ignoreCase: true);
            var matches = await _matchDal.GetMatchesByStatusAsync(statusEnum);
            return _mapper.Map<List<ResultMatchDto>>(matches);
        }
        public async Task<MatchDetailDto?> GetMatchDetailAsync(int id)
        {
            var match = await _matchDal.GetMatchWithDetailsByIdAsync(id);
            return match == null ? null : _mapper.Map<MatchDetailDto>(match);
        }


    }
}