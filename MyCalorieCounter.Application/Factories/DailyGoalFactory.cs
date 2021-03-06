using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Factories
{
    public class DailyGoalFactory : IDailyGoalFactory
    {
        public DailyGoal CreateDailyGoal(DailyGoalDto dailyGoalDto)
        {
            return new DailyGoal()
            {
                UserId = dailyGoalDto.UserId,
                Calories = dailyGoalDto.Calories,
                Proteins = dailyGoalDto.Proteins,
                Carbs = dailyGoalDto.Carbs,
                Fats = dailyGoalDto.Fats
            };
        }
        public DailyGoal CreateDailyGoal(string userID, double cal, double pro, double carb, double fat)
        {
            return new DailyGoal()
            {
                UserId = userID,
                Calories = cal,
                Proteins = pro,
                Carbs = carb,
                Fats = fat
            };
        }
        public DailyGoal CreateNewUsersDailyGoal(string userId)
        {
            return new DailyGoal()
            {
                UserId = userId,
                Calories = 0,
                Proteins = 0,
                Carbs = 0,
                Fats = 0
            };
        }

        public DailyGoalDto CreateDailyGoalDto(DailyGoal dailyGoal)
        {
            return new DailyGoalDto()
            {
                Id = dailyGoal.Id,
                UserId = dailyGoal.UserId,
                Calories = dailyGoal.Calories,
                Proteins = dailyGoal.Proteins,
                Carbs = dailyGoal.Carbs,
                Fats = dailyGoal.Fats
            };
        }
        public DailyGoalDto CreateNewUsersDailyGoalDto(string userId)
        {
            return new DailyGoalDto()
            {
                UserId = userId,
                Calories = 0,
                Proteins = 0,
                Carbs = 0,
                Fats = 0
            };
        }
    }
}
