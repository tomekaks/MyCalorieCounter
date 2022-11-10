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
    public class MealService : IMealService
    {
        private readonly IMealFactory _mealFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMealDtoValidator _mealDtoValidator;

        public MealService(IMealFactory mealFactory, IUnitOfWork unitOfWork, IMealDtoValidator mealDtoValidator)
        {
            _mealFactory = mealFactory;
            _unitOfWork = unitOfWork;
            _mealDtoValidator = mealDtoValidator;
        }

        public async Task AddMeal(MealDto mealDto)
        {
            var validationResult = _mealDtoValidator.Validate(mealDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationExeption(validationResult);
            }

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
        public async Task DeleteMeal(int id)
        {
            var meal = await _unitOfWork.Meals.Get(m => m.Id == id);
            _unitOfWork.Meals.Delete(meal);
            await _unitOfWork.Save();
        }
        public async Task<List<MealDto>> GetTodaysMeals(string userId, string date)
        {
            var list = await _unitOfWork.Meals.GetAll(m => m.UserId == userId && m.Date == date,
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

            var meal = await _unitOfWork.Meals.Get(q => q.Id == mealDto.Id);
            meal = _mealFactory.MapToModel(meal, mealDto);

            _unitOfWork.Meals.Update(meal);
            await _unitOfWork.Save();
        }
    }
}
