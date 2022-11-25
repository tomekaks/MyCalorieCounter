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
        
        public DailyGoal MapToModel(DailyGoal model, DailyGoalDto dto)
        {
            model.Calories = dto.Calories;
            model.Proteins = dto.Proteins;
            model.Carbs = dto.Carbs;
            model.Fats = dto.Fats;

            return model;
        }
    }
}
