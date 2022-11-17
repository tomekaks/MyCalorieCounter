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

        public async Task<DailySumDto> GetDailySum(string userId)
        {
            var todaysDate = GetTodaysDate();

            var dailySum = await _unitOfWork.DailySums.Get(q => q.Date == todaysDate && q.UserId == userId);
            if (dailySum == null)
            {
                return await BeginNewDailySum(todaysDate, userId);
            }
            return _dailySumFactory.CreateDailySumDto(dailySum);
        }

        public async Task UpdateDailySum(DailySumDto dailySumDto)
        {
            var dailySum = await _unitOfWork.DailySums.Get(q => q.Id == dailySumDto.Id);
            dailySum = _dailySumFactory.MapToModel(dailySum, dailySumDto);

            _unitOfWork.DailySums.Update(dailySum);
            await _unitOfWork.Save();
        }

        private async Task<DailySumDto> BeginNewDailySum(string todaysDate, string userId)
        {
            var dailySum = _dailySumFactory.CreateDailySum(todaysDate, userId);
            await _unitOfWork.DailySums.Add(dailySum);
            await _unitOfWork.Save();

            dailySum = await _unitOfWork.DailySums.Get(q => q.Date == todaysDate && q.UserId == userId);
            return _dailySumFactory.CreateDailySumDto(dailySum);
        }

        private string GetTodaysDate()
        {
            return DateTime.Today.ToString("d");
        }
    }
}
