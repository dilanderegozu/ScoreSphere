using AutoMapper;
using ScoreSphere.BusinessLayer.Abstract;
using ScoreSphere.DataAccessLayer.Abstract;
using ScoreSphere.DtoLayer.LeagueDtos;
using ScoreSphere.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.BusinessLayer.Concrete
{
    public class LeagueManager : ILeagueService
    {
        private readonly ILeagueDal _leagueDal;
        private readonly IMapper _mapper;

        public LeagueManager(ILeagueDal leagueDal, IMapper mapper)
        {
            _leagueDal = leagueDal;
            _mapper = mapper;
        }

        public async Task TDeleteAsync(int id)
        {
            await _leagueDal.DeleteAsync(id);
        }

        public async Task<ResultLeagueDto?> TGetByIdAsync(int id)
        {
            var league = await _leagueDal.GetByIdAsync(id);
            return _mapper.Map<ResultLeagueDto>(league);
        }

        public async Task<List<ResultLeagueDto>> TGetListAsync()
        {
            var league = await _leagueDal.GetListAsync();
            return _mapper.Map<List<ResultLeagueDto>>(league);
        }

        public async Task TInsertAsync(CreateLeagueDto dto)
        {
            var value = _mapper.Map<League>(dto);
            await _leagueDal.InsertAsync(value);
        }

        public async Task TUpdateAsync(UpdateLeagueDto dto)
        {
            var value = _mapper.Map<League>(dto);
            await _leagueDal.UpdateAsync(value);
        }
        
    }
}
