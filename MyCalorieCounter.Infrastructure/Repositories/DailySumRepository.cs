using MyCalorieCounter.Application.Interfaces.Repositories;
using MyCalorieCounter.Core.Data;
using MyCalorieCounter.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Infrastructure.Repositories
{
    public class DailySumRepository : GenericRepository<DailySum>, IDailySumRepository
    {
        private readonly ApplicationDbContext _context;

        public DailySumRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Update(DailySum dailySum)
        {
            var obj = await _context.DailySums.FirstOrDefaultAsync(d => d.Date == dailySum.Date && d.UserId == dailySum.UserId);
            if (obj != null)
            {
                obj.Calories = dailySum.Calories;
                obj.Proteins = dailySum.Proteins;
                obj.Carbs = dailySum.Carbs;
                obj.Fats = dailySum.Fats;
                obj.CaloriesBurned = dailySum.CaloriesBurned;
            }
        }
    }
}
