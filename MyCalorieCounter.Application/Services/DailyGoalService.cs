using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Application.Interfaces.Repositories;
using MyCalorieCounter.Application.Interfaces.Services;
using MyCalorieCounter.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Services
{
    public class DailyGoalService : IDailyGoalService
    {
        private readonly IDailyGoalFactory _dailyGoalFactory;
        private readonly IUnitOfWork _unitOfWork;

        public DailyGoalService(IDailyGoalFactory dailyGoalFactory, IUnitOfWork unitOfWork)
        {
            _dailyGoalFactory = dailyGoalFactory;
            _unitOfWork = unitOfWork;
        }

        public async Task<DailyGoalDto> GetDailyGoal(string userId)
        {
            var dailyGoal = await _unitOfWork.DailyGoals.GetById(userId);
            if (dailyGoal == null)
            {
                return await SetNewUsersDailyGoal(userId);
            }
            else
            {
                return _dailyGoalFactory.CreateDailyGoalDto(dailyGoal);
            }  
        }
        public async Task UpdateDailyGoal(DailyGoalDto dailyGoalDto)
        {
            var dailyGoal = _dailyGoalFactory.CreateDailyGoal(dailyGoalDto);
            await _unitOfWork.DailyGoals.Update(dailyGoal);
            await _unitOfWork.Save();
        }
        public async Task UpdateDailyGoal(string userId, double cal, double pro, double carb, double fat)
        {
            var dailyGoal = _dailyGoalFactory.CreateDailyGoal(userId, cal, pro, carb, fat);
            await _unitOfWork.DailyGoals.Update(dailyGoal);
            await _unitOfWork.Save();
        }

        private async Task<DailyGoalDto> SetNewUsersDailyGoal(string userId)
        {
            var dailyGoal = _dailyGoalFactory.CreateNewUsersDailyGoal(userId);
            await _unitOfWork.DailyGoals.Add(dailyGoal);
            await _unitOfWork.Save();
            return _dailyGoalFactory.CreateNewUsersDailyGoalDto(userId);
        }
    }
}
