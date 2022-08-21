using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Interfaces.Factories
{
    public interface IDailySumFactory
    {
        DailySum CreateDailySum(DailySumDto dailySumDto);
        DailySumDto CreateDailySumDto(DailySum dailySum);
        DailySumDto CreateDailySumDto(string todaysDate, string userId);
    }
}
