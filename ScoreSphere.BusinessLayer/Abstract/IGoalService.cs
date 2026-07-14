using Microsoft.EntityFrameworkCore.Update;
using ScoreSphere.DtoLayer.GoalDtos;
using ScoreSphere.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreSphere.BusinessLayer.Abstract
{
    public interface IGoalService
    {
        Task<List<ResultGoalDto>> TGetListAsync();
        Task<ResultGoalDto?> TGetByIdAsync(int id);
        Task TInsertAsync(CreateGoalDto dto);
        Task TDeleteAsync(int id);
    }
}
