using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IDailySumRepository DailySums { get; }
        IMealRepository Meals { get; }
        IDailyGoalRepository DailyGoals { get; }
        IExerciseRepository Exercises { get; }
        IMyActivityRepository MyActivities { get; }
        Task Save();
    }
}
