using AutoMapper;
using ScoreSphere.BusinessLayer.Abstract;
using ScoreSphere.DataAccessLayer.Abstract;
using ScoreSphere.DtoLayer.SeasonDtos;
using ScoreSphere.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.BusinessLayer.Concrete
{
    public class SeasonManager : ISeasonService
    {
        private readonly ISeasonDal _seasonDal;
        private readonly IMapper _mapper;

        public SeasonManager(ISeasonDal seasonDal, IMapper mapper)
        {
            _seasonDal = seasonDal;
            _mapper = mapper;
        }

        public async Task TDeleteAsync(int id)
        {
            await _seasonDal.DeleteAsync(id);
        }

        public async Task<ResultSeasonDto?> TGetByIdAsync(int id)
        {
            var season = await _seasonDal.GetByIdAsync(id);
            return _mapper.Map<ResultSeasonDto>(season);
        }

        public async Task<List<ResultSeasonDto>> TGetListAsync()
        {
            var seasons = await _seasonDal.GetListAsync();
            return seasons.Select(_mapper.Map<ResultSeasonDto>).ToList();
        }

        public async Task TInsertAsync(CreateSeasonDto dto)
        {
            var season = _mapper.Map<Season>(dto);
            await _seasonDal.InsertAsync(season);
        }

        public async Task TUpdateAsync(UpdateSeasonDto dto)
        {
            var season = _mapper.Map<Season>(dto);
            await _seasonDal.UpdateAsync(season);
        }
        }
    }
