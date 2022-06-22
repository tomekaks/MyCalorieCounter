using Microsoft.EntityFrameworkCore;
using MyCalorieCounter.Application.Interfaces.Repositories;
using MyCalorieCounter.Core.Data;
using MyCalorieCounter.Infrastructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Infrastructure.Repositories
{
    public class DailyGoalRepository : IDailyGoalRepository
    {
        private readonly ApplicationDbContext _context;

        public DailyGoalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DailyGoal> GetById(string userId)
        {
            var dailyGoal = await _context.DailyGoals.FirstOrDefaultAsync(d => d.UserId == userId);
            return dailyGoal;
        }
        public async Task Update(DailyGoal dailyGoal)
        {
            var obj = await _context.DailyGoals.FirstOrDefaultAsync(q => q.UserId == dailyGoal.UserId);
            if (obj != null)
            {
                _context.DailyGoals.Update(obj);
            }
        }
        public async Task Add(DailyGoal dailyGoal)
        {
            await _context.DailyGoals.AddAsync(dailyGoal);
        }
    }
}
