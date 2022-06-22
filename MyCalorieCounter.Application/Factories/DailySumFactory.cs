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
        public DailySum CreateDailySum(DailySumDto today)
        {
            return new DailySum()
            {
                UserId = today.UserId,
                Date = today.Date,
                Calories = today.Calories,
                Proteins = today.Proteins,
                Carbs = today.Carbs,
                Fats = today.Fats
            };
        }
        public DailySumDto CreateDailySumDto(DailySum today)
        {
            return new DailySumDto()
            {
                Id = today.Id,
                UserId = today.UserId,
                Date = today.Date,
                Calories = today.Calories,
                Proteins = today.Proteins,
                Carbs = today.Carbs,
                Fats = today.Fats
            };
        }
        public DailySumDto CreateDailySumDto(string todaysDate)
        {
            return new DailySumDto()
            {
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
