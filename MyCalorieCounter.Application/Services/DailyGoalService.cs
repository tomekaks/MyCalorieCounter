using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Exeptions;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Application.Interfaces.Repositories;
using MyCalorieCounter.Application.Interfaces.Services;
using MyCalorieCounter.Application.Interfaces.Validators;
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
        private readonly IDailyGoalDtoValidator _dailyGoalDtoValidator;

        public DailyGoalService(IDailyGoalFactory dailyGoalFactory, IUnitOfWork unitOfWork, IDailyGoalDtoValidator dailyGoalDtoValidator)
        {
            _dailyGoalFactory = dailyGoalFactory;
            _unitOfWork = unitOfWork;
            _dailyGoalDtoValidator = dailyGoalDtoValidator;
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
            var validationResult = _dailyGoalDtoValidator.Validate(dailyGoalDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationExeption(validationResult);
            }

            var dailyGoal = await _unitOfWork.DailyGoals.GetById(dailyGoalDto.UserId);
            dailyGoal = _dailyGoalFactory.MapToModel(dailyGoal, dailyGoalDto);

            _unitOfWork.DailyGoals.Update(dailyGoal);
            await _unitOfWork.Save();
        }
        public async Task UpdateDailyGoal(string userId, double cal, double pro, double carb, double fat)
        {
            var dailyGoalDto = _dailyGoalFactory.CreateDailyGoalDto(userId, cal, pro, carb, fat);
            
            var validationResult = _dailyGoalDtoValidator.Validate(dailyGoalDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationExeption(validationResult);
            }

            var dailyGoal = await _unitOfWork.DailyGoals.GetById(dailyGoalDto.UserId);
            dailyGoal = _dailyGoalFactory.MapToModel(dailyGoal, dailyGoalDto);

            _unitOfWork.DailyGoals.Update(dailyGoal);
            await _unitOfWork.Save();
        }

        private async Task<DailyGoalDto> SetNewUsersDailyGoal(string userId)
        {
            var dailyGoal = _dailyGoalFactory.CreateNewUsersDailyGoal(userId);
            await _unitOfWork.DailyGoals.Add(dailyGoal);
            await _unitOfWork.Save();

            dailyGoal = await _unitOfWork.DailyGoals.GetById(userId);

            return _dailyGoalFactory.CreateDailyGoalDto(dailyGoal);
        }
    }
}
