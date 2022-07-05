using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Application.Interfaces.Repositories;
using MyCalorieCounter.Application.Interfaces.Services;
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

        public MealService(IMealFactory mealFactory, IUnitOfWork unitOfWork)
        {
            _mealFactory = mealFactory;
            _unitOfWork = unitOfWork;
        }

        public async Task AddMeal(MealDto mealDto)
        {
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
        public async Task UpdateMeal(MealDto mealDto, int id)
        {
            var meal = _mealFactory.CreateMeal(mealDto, id);
            await _unitOfWork.Meals.Update(meal);
            await _unitOfWork.Save();
        }
    }
}
