using MyCalorieCounter.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Interfaces.Repositories
{
    public interface IDailyGoalRepository
    {
        void Update(DailyGoal dailyGoal);
        Task<DailyGoal> GetById(string userId);
        Task Add(DailyGoal dailyGoal);
    }
}
