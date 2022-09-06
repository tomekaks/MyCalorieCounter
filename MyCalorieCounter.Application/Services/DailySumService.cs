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

        public DailySumService(IUnitOfWork unitOfWork, IDailySumFactory dailySumFactory)
        {
            _unitOfWork = unitOfWork;
            _dailySumFactory = dailySumFactory;
        }

        public async Task BeginNewOrUpdateDailySum(DailySumDto dailySumDto)
        {
            var isExists = await _unitOfWork.DailySums.IsExists(d => d.Date == dailySumDto.Date && d.UserId == dailySumDto.UserId);
            if (isExists)
            {
                await UpdateDailySum(dailySumDto);
            }
            else
            {
                await BeginNewDay(dailySumDto);
            }
        }

        public async Task<DailySumDto> GetDailySum(string userId)
        {
            var todaysDate = GetTodaysDate();

            var dailySum = await _unitOfWork.DailySums.Get(q => q.Date == todaysDate && q.UserId == userId);
            if (dailySum == null)
            {  
                return _dailySumFactory.CreateDailySumDto(todaysDate, userId);
            }
            return _dailySumFactory.CreateDailySumDto(dailySum);
        }
        private async Task UpdateDailySum(DailySumDto dailySumDto)
        {
            var dailySum = _dailySumFactory.CreateDailySum(dailySumDto);
            await _unitOfWork.DailySums.Update(dailySum);
            await _unitOfWork.Save();
        }
        private async Task BeginNewDay(DailySumDto dailySumDto)
        {
            var dailySum = _dailySumFactory.CreateDailySum(dailySumDto);
            await _unitOfWork.DailySums.Add(dailySum);
            await _unitOfWork.Save();
        }
        private string GetTodaysDate()
        {
            return DateTime.Today.ToString("d");
        }
    }
}
