using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Interfaces.Factories
{
    public interface IMealFactory
    {
        Meal CreateMeal(MealDto mealDto);
        Meal CreateMeal(MealDto mealDto, int id);
        MealDto CreateMealDto(Meal meal);
        public List<MealDto> CreateMealDtoList(List<Meal> mealList);
        Meal MapToModel(Meal model, MealDto dto);
    }
}
