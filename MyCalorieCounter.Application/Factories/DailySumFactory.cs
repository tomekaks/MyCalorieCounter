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
    public class DailySumFactory : IDailySumFactory
    {
        public DailySum CreateDailySum(DailySumDto dailySumDto)
        {
            return new DailySum()
            {
                UserId = dailySumDto.UserId,
                Date = dailySumDto.Date,
                Calories = dailySumDto.Calories,
                Proteins = dailySumDto.Proteins,
                Carbs = dailySumDto.Carbs,
                Fats = dailySumDto.Fats
            };
        }
        public DailySumDto CreateDailySumDto(DailySum dailySum)
        {
            return new DailySumDto()
            {
                Id = dailySum.Id,
                UserId = dailySum.UserId,
                Date = dailySum.Date,
                Calories = dailySum.Calories,
                Proteins = dailySum.Proteins,
                Carbs = dailySum.Carbs,
                Fats = dailySum.Fats
            };
        }
        public DailySumDto CreateDailySumDto(string todaysDate, string userId)
        {
            return new DailySumDto()
            {
                UserId = userId,
                Date = todaysDate,
                Calories = 0,
                Proteins = 0,
                Carbs = 0,
                Fats = 0
            };
        }
        public List<DailySumDto> CreateDailySumDtoList(List<DailySum> dailySums)
        {
            var listDto = new List<DailySumDto>();
            foreach (var item in dailySums)
            {
                listDto.Add(CreateDailySumDto(item));
            }
            return listDto;
        }
    }
}
