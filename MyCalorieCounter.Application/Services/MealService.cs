using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Exeptions;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Application.Interfaces.Repositories;
using MyCalorieCounter.Application.Interfaces.Services;
using MyCalorieCounter.Application.Interfaces.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Services
{
    [Obsolete]
    public class MealService : IMealService
    {
        private readonly IMealFactory _mealFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMealDtoValidator _mealDtoValidator;
        private readonly IProductService _productService;
        private readonly IDailySumService _dailySumService;

        public MealService(IMealFactory mealFactory, IUnitOfWork unitOfWork, IMealDtoValidator mealDtoValidator, IProductService productService, IDailySumService dailySumService)
        {
            _mealFactory = mealFactory;
            _unitOfWork = unitOfWork;
            _mealDtoValidator = mealDtoValidator;
            _productService = productService;
            _dailySumService = dailySumService;
        }

        public async Task AddMeal(MealDto mealDto)
        {
            var validationResult = _mealDtoValidator.Validate(mealDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationExeption(validationResult);
            }

            var productDto = await _productService.GetProduct(mealDto.ProductId);
            var dailySumDto = await _dailySumService.GetDailySum(mealDto.UserId);

            mealDto.Calories = (productDto.Calories * mealDto.Weight) / 100;
            mealDto.Proteins = (productDto.Proteins * mealDto.Weight) / 100;
            mealDto.Carbs = (productDto.Carbs * mealDto.Weight) / 100;
            mealDto.Fats = (productDto.Fats * mealDto.Weight) / 100;
            mealDto.DailySumId = dailySumDto.Id;
            mealDto.Date = dailySumDto.Date;

            dailySumDto.Calories += mealDto.Calories;
            dailySumDto.Proteins += mealDto.Proteins;
            dailySumDto.Carbs += mealDto.Carbs;
            dailySumDto.Fats += mealDto.Carbs;
            await _dailySumService.UpdateDailySum(dailySumDto);

            var meal = _mealFactory.CreateMeal(mealDto);
            
            await _unitOfWork.Meals.Add(meal);
            await _unitOfWork.Save();
        }
        public async Task DeleteMeal(MealDto mealDto)
        {
            var meal = _mealFactory.CreateMeal(mealDto);
            _unitOfWork.Meals.Delete(meal);
            await _unitOfWork.Save();
        }
        public async Task DeleteMeal(int id, string userId)
        {
            var mealDto = await GetMeal(id);
            var dailySumDto = await _dailySumService.GetDailySum(userId);

            dailySumDto.Calories -= mealDto.Calories;
            dailySumDto.Proteins -= mealDto.Proteins;
            dailySumDto.Carbs -= mealDto.Carbs;
            dailySumDto.Fats -= mealDto.Fats;
            await _dailySumService.UpdateDailySum(dailySumDto);

            var meal = _mealFactory.CreateMeal(mealDto);
            _unitOfWork.Meals.Delete(meal);
            await _unitOfWork.Save();
        }
        public async Task<List<MealDto>> GetTodaysMeals(int dailySumId)
        {
            var list = await _unitOfWork.Meals.GetAll(m => m.DailySumId == dailySumId,
                                                      includeProperties:"Product");
            var mealList = _mealFactory.CreateMealDtoList(list.ToList());
            return mealList;
        }
        public async Task<MealDto> GetMeal(int id)
        {
            var meal = await _unitOfWork.Meals.Get(m => m.Id == id, includeProperties:"Product");
            return _mealFactory.CreateMealDto(meal);
        }
        public async Task UpdateMeal(MealDto mealDto)
        {
            var validationResult = _mealDtoValidator.Validate(mealDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationExeption(validationResult);
            }

            var dailySumDto = await _dailySumService.GetDailySum(mealDto.UserId);
            var meal = await _unitOfWork.Meals.Get(q => q.Id == mealDto.Id);

            dailySumDto.Calories -= meal.Calories;
            dailySumDto.Proteins -= meal.Proteins;
            dailySumDto.Carbs -= meal.Carbs;
            dailySumDto.Fats -= meal.Fats;

            dailySumDto.Calories += mealDto.Calories;
            dailySumDto.Proteins += mealDto.Proteins;
            dailySumDto.Carbs += mealDto.Carbs;
            dailySumDto.Fats += mealDto.Fats;

            await _dailySumService.UpdateDailySum(dailySumDto);

            meal = _mealFactory.MapToModel(meal, mealDto);

            _unitOfWork.Meals.Update(meal);
            await _unitOfWork.Save();
        }
    }
}
