using MyCalorieCounter.Application.Interfaces.Repositories;
using MyCalorieCounter.Infrastructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Products = new ProductRepository(context);
            DailySums = new DailySumRepository(context);
            Settings = new SettingRepository(context);
            Meals = new MealRepository(context);
            DailyGoals = new DailyGoalRepository(context);
            Exercises = new ExerciseRepository(context);
            MyActivities = new MyActivityRepository(context);
        }

        public IProductRepository Products { get; private set; }
        public IDailySumRepository DailySums { get; private set; }
        public ISettingRepository Settings { get; private set; }
        public IMealRepository Meals { get; private set; }
        public IDailyGoalRepository DailyGoals { get; private set; }
        public IExerciseRepository Exercises { get; private set; }
        public IMyActivityRepository MyActivities { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
