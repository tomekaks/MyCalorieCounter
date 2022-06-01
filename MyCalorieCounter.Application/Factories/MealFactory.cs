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
    public class MealFactory : IMealFactory
    {
        public Meal CreateMeal(MealDto mealDto)
        {
            return new Meal()
            {
                ProductId = mealDto.ProductId,
                DailySumId = mealDto.DailySumId,
                Weight = mealDto.Weight
            };
        }

        public MealDto CreateMealDto(Meal meal)
        {
            return new MealDto()
            {
                Id = meal.Id,
                ProductId = meal.ProductId,
                DailySumId = meal.DailySumId,
                Weight = meal.Weight
            };
        }

        public List<MealDto> CreateMealDtoList(List<Meal> mealList)
        {
            var meals = new List<MealDto>();
            foreach (var item in mealList)
            {
                meals.Add(CreateMealDto(item));
            }
            return meals;
        }
    }
}
