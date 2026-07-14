using AutoMapper;
using ScoreSphere.BusinessLayer.Abstract;
using ScoreSphere.DataAccessLayer.Abstract;
using ScoreSphere.DtoLayer.MatchEventDtos;
using ScoreSphere.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.BusinessLayer.Concrete
{
    public class MatchEventManager : IMatchEventService
    {
        private readonly IMatchEventDal _matchEventDal;
        private readonly IMapper _mapper;

        public MatchEventManager(IMatchEventDal matchEventDal, IMapper mapper)
        {
            _matchEventDal = matchEventDal;
            _mapper = mapper;
        }

        public async Task TDeleteAsync(int id)
        {
            await _matchEventDal.DeleteAsync(id);
        }

        public async Task<ResultMatchEventDto?> TGetByIdAsync(int id)
        {
            var matchEvent = await _matchEventDal.GetByIdAsync(id);
            return _mapper.Map<ResultMatchEventDto>(matchEvent);
        }

        public async Task<List<ResultMatchEventDto>> TGetListAsync()
        {
            var matchEvents = await _matchEventDal.GetListAsync();
            return _mapper.Map<List<ResultMatchEventDto>>(matchEvents);
        }

        public async Task TInsertAsync(CreateMatchEventDto dto)
        {
            var value = _mapper.Map<MatchEvent>(dto);
            await _matchEventDal.InsertAsync(value);
        }
    }
}