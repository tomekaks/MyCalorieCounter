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
    public class DailySumService : IDailySumService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDailySumFactory _dailySumFactory;
        private bool newDay = true;

        public DailySumService(IUnitOfWork unitOfWork, IDailySumFactory dailySumFactory)
        {
            _unitOfWork = unitOfWork;
            _dailySumFactory = dailySumFactory;
        }

        public async Task BeginNewOrUpdateTodaysMacros(DailySumDto todaysMacros)
        {
            if (newDay == true)
            {
                await BeginNewDay(todaysMacros);
            }
            else
                await UpdateTodaysMacros(todaysMacros);
        }

        public async Task<DailySumDto> GetTodaysMacros()
        {
            var todaysDate = GetTodaysDate();

            var todaysSum = await _unitOfWork.DailySums.Get(q => q.Date == todaysDate);
            if (todaysSum == null)
            {
                newDay = true;
                return _dailySumFactory.CreateDailySumDto(todaysDate);
            }
            newDay = false;
            return _dailySumFactory.CreateDailySumDto(todaysSum);
        }
        private async Task UpdateTodaysMacros(DailySumDto todaysMacros)
        {
            var today = _dailySumFactory.CreateDailySum(todaysMacros);
            await _unitOfWork.DailySums.Update(today);
            await _unitOfWork.Save();
        }
        private async Task BeginNewDay(DailySumDto todaysMacros)
        {
            var today = _dailySumFactory.CreateDailySum(todaysMacros);
            await _unitOfWork.DailySums.Add(today);
            await _unitOfWork.Save();
        }
        private string GetTodaysDate()
        {
            return DateTime.Today.ToString("d");
        }
    }
}
