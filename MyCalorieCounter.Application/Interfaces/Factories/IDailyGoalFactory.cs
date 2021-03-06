using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Interfaces.Factories
{
    public interface IDailyGoalFactory
    {
        DailyGoal CreateDailyGoal(DailyGoalDto dailyGoalDto);
        DailyGoal CreateDailyGoal(string userID, double cal, double pro, double carb, double fat);
        DailyGoalDto CreateDailyGoalDto(DailyGoal dailyGoal);
        DailyGoal CreateNewUsersDailyGoal(string userId);
        DailyGoalDto CreateNewUsersDailyGoalDto(string userId);
    }
}
