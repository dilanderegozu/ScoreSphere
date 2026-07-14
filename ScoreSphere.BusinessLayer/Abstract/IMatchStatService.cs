using ScoreSphere.DtoLayer.MatchStatDtos;
using ScoreSphere.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.BusinessLayer.Abstract
{
    public interface IMatchStatService

    {
        Task<List<ResultMatchStatDto>> TGetListAsync();
        Task<ResultMatchStatDto ?> TGetByIdAsync(int id);
        Task TInsertAsync(CreateMatchStatDto dto);
        Task TUpdateAsync(UpdateMatchStatDto dto);
        Task TDeleteAsync(int id);
    }
}
