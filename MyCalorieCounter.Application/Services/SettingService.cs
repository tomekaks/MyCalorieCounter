using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Interfaces.Repositories;
using MyCalorieCounter.Application.Interfaces.Services;
using MyCalorieCounter.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Services
{
    public class SettingService : ISettingService
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly string _calKey = "Calories";
        public readonly string _protsKey = "Proteins";
        public readonly string _carbsKey = "Carbs";
        public readonly string _fatsKey = "Fats";

        public SettingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DailyGoals> GetDailyGoals()
        {
            var calories = await _unitOfWork.Settings.Get(q => q.Key == _calKey);
            var proteins = await _unitOfWork.Settings.Get(q => q.Key == _protsKey);
            var carbs = await _unitOfWork.Settings.Get(q => q.Key == _carbsKey);
            var fats = await _unitOfWork.Settings.Get(q => q.Key == _fatsKey);
            return new DailyGoals()
            {
                Calories = double.Parse(calories.Value),
                Proteins = double.Parse(proteins.Value),
                Carbs = double.Parse(carbs.Value),
                Fats = double.Parse(fats.Value)
            };
        }

        public async Task UpdateDailyGoals(DailyGoals dailyGoals)
        {
            var calories = CreateSetting(_calKey, dailyGoals.Calories.ToString());
            var proteins = CreateSetting(_protsKey, dailyGoals.Proteins.ToString());
            var carbs = CreateSetting(_carbsKey, dailyGoals.Carbs.ToString());
            var fats = CreateSetting(_fatsKey, dailyGoals.Fats.ToString());
            await _unitOfWork.Settings.Update(calories);
            await _unitOfWork.Settings.Update(proteins);
            await _unitOfWork.Settings.Update(carbs);
            await _unitOfWork.Settings.Update(fats);
            await _unitOfWork.Save();
        }

        private Setting CreateSetting(string key, string value)
        {
            return new Setting()
            {
                Key = key,
                Value = value
            };
        }
    }
}
