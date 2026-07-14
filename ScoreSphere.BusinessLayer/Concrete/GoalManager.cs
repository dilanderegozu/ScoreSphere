using AutoMapper;
using ScoreSphere.BusinessLayer.Abstract;
using ScoreSphere.DataAccessLayer.Abstract;
using ScoreSphere.DtoLayer.GoalDtos;
using ScoreSphere.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.BusinessLayer.Concrete
{
    public class GoalManager : IGoalService
    {
        private readonly IGoalDal _goalDal;
        private readonly IMapper _mapper;

        public GoalManager(IGoalDal goalDal, IMapper mapper)
        {
            _goalDal = goalDal;
            _mapper = mapper;
        }

        public async Task TDeleteAsync(int id)
        {
            await _goalDal.DeleteAsync(id);
        }

        public async Task<ResultGoalDto?> TGetByIdAsync(int id)
        {
            var goal = await _goalDal.GetByIdAsync(id);
            return _mapper.Map<ResultGoalDto>(goal);
        }

        public async Task<List<ResultGoalDto>> TGetListAsync()
        {
            var goal = await _goalDal.GetListAsync();
            return _mapper.Map<List<ResultGoalDto>>(goal);
        }

        public async Task TInsertAsync(CreateGoalDto dto)
        {
            var goal = _mapper.Map<Goal>(dto);
            await _goalDal.InsertAsync(goal);
        }
    }
}
