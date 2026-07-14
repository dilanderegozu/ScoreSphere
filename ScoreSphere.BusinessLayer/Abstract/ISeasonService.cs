using ScoreSphere.DtoLayer.SeasonDtos;
using ScoreSphere.DtoLayer.TeamDtos;
using ScoreSphere.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.BusinessLayer.Abstract
{
    public interface ISeasonService 
    {
        Task<List<ResultSeasonDto>> TGetListAsync();
        Task<ResultSeasonDto?> TGetByIdAsync(int id);
        Task TInsertAsync(CreateSeasonDto dto);
        Task TUpdateAsync(UpdateSeasonDto dto);
        Task TDeleteAsync(int id);
    }
}
