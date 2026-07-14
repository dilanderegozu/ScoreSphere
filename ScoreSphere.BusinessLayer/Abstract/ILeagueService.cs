using Microsoft.EntityFrameworkCore.Diagnostics;
using ScoreSphere.DtoLayer.LeagueDtos;
using ScoreSphere.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.BusinessLayer.Abstract
{
    public interface ILeagueService
    {
        Task<List<ResultLeagueDto>> TGetListAsync();
        Task<ResultLeagueDto?> TGetByIdAsync(int id);
        Task TInsertAsync(CreateLeagueDto dto);
        Task TUpdateAsync(UpdateLeagueDto dto);
        Task TDeleteAsync(int id);
    }
}
