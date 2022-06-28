﻿using MyCalorieCounter.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Interfaces.Services
{
    public interface IMealService
    {
        Task AddMeal(MealDto mealDto);
        Task DeleteMeal(MealDto mealDto);
        Task<List<MealDto>> GetTodaysMeals(string userId, string date);
    }
}
