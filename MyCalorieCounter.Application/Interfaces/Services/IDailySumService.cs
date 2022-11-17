using MyCalorieCounter.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Interfaces.Services
{
    public interface IDailySumService
    {
        Task<DailySumDto> GetDailySum(string userId);
        Task UpdateDailySum(DailySumDto dailySumDto);

    }
}
