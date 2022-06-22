using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Interfaces.Services
{
    public interface IDailyGoalService
    {
        Task UpdateDailyGoal(DailyGoalDto dailyGoalDto);
        Task UpdateDailyGoal(string userId, double cal, double pro, double carb, double fat);
        Task<DailyGoalDto> GetDailyGoal(string userId);
    }
}
