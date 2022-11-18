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
        private readonly IProductFactory _productFactory;

        public MealFactory(IProductFactory productFactory)
        {
            _productFactory = productFactory;
        }

        public Meal CreateMeal(MealDto mealDto)
        {
            return new Meal()
            {
                UserId = mealDto.UserId,
                ProductId = mealDto.ProductId,
                DailySumId = mealDto.DailySumId,
                Date = mealDto.Date,
                Weight = mealDto.Weight,
                Calories = mealDto.Calories,
                Proteins = mealDto.Proteins,
                Carbs = mealDto.Carbs,
                Fats = mealDto.Fats
            };
        }
        public Meal CreateMeal(MealDto mealDto, int id)
        {
            return new Meal()
            {
                Id = id,
                UserId = mealDto.UserId,
                ProductId = mealDto.ProductId,
                DailySumId = mealDto.DailySumId,
                Date = mealDto.Date,
                Weight = mealDto.Weight,
                Calories = mealDto.Calories,
                Proteins = mealDto.Proteins,
                Carbs = mealDto.Carbs,
                Fats = mealDto.Fats
            };
        }

        public MealDto CreateMealDto(Meal meal)
        {
            return new MealDto()
            {
                Id = meal.Id,
                UserId = meal.UserId,
                ProductId = meal.ProductId,
                Product = _productFactory.CreateProductDto(meal.Product),
                DailySumId = meal.DailySumId,
                Date = meal.Date,
                Weight = meal.Weight,
                Calories = meal.Calories,
                Proteins = meal.Proteins,
                Carbs = meal.Carbs,
                Fats = meal.Fats
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

        public Meal MapToModel(Meal model, MealDto dto)
        {
            model.Weight = dto.Weight;
            model.Calories = dto.Calories;
            model.Proteins = dto.Proteins;
            model.Carbs = dto.Carbs;
            model.Fats = dto.Fats;
            return model;
        }
    }
}
