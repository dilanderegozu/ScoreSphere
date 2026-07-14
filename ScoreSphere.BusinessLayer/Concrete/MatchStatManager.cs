using AutoMapper;
using ScoreSphere.BusinessLayer.Abstract;
using ScoreSphere.DataAccessLayer.Abstract;
using ScoreSphere.DtoLayer.MatchStatDtos;
using ScoreSphere.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.BusinessLayer.Concrete
{
    public class MatchStatManager : IMatchStatService
    {
        private readonly IMatchStatDal _matchStatDal; 
        private readonly IMapper _mapper;

        public MatchStatManager(IMatchStatDal matchStatDal, IMapper mapper)
        {
            _matchStatDal = matchStatDal;
            _mapper = mapper;
        }

        public async Task TDeleteAsync(int id)
        {
            await _matchStatDal.DeleteAsync(id);
        }

        public async Task<ResultMatchStatDto?> TGetByIdAsync(int id)
        {
            var matchStat = await _matchStatDal.GetByIdAsync(id);
            return _mapper.Map<ResultMatchStatDto?>(matchStat);
        }

        public async Task<List<ResultMatchStatDto>> TGetListAsync()
        {
            var matchStats = await _matchStatDal.GetListAsync();
            return matchStats.Select(_mapper.Map<ResultMatchStatDto>).ToList();
        }

        public async Task TInsertAsync(CreateMatchStatDto dto)
        {
            var matchStat = _mapper.Map<MatchStat>(dto);
            await _matchStatDal.InsertAsync(matchStat);
        }

        public async Task TUpdateAsync(UpdateMatchStatDto dto)
        {
            var matchStat = _mapper.Map<MatchStat>(dto);
            await _matchStatDal.UpdateAsync(matchStat);
        }
    }
}

