using ScoreSphere.DtoLayer.MatchEventDtos;
using ScoreSphere.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.BusinessLayer.Abstract
{
    public interface IMatchEventService 
    {
        Task<List<ResultMatchEventDto>> TGetListAsync();
        Task<ResultMatchEventDto?> TGetByIdAsync(int id);
        Task TInsertAsync(CreateMatchEventDto dto);
        Task TDeleteAsync(int id);
    }
}
