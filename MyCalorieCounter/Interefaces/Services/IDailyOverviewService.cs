using MyCalorieCounter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalorieCounter.Interefaces.Services
{
    public interface IDailyOverviewService
    {
        Task<DailyOverviewVM> GetDailyOverview(string userId);
        Task<DailyGoalVM> GenerateDailyGoalVM(string userId);
        Task UpdateDailyGoal(DailyGoalVM model);
        Task<DeleteMealVM> GenerateDeleteMealVM(int id);
        Task DeleteMeal(int id);
        Task<EditMealVM> GetMeal(int id);
        Task EditMeal(EditMealVM model);
        Task<DeleteActivityVM> GenerateDeleteActivityVM(int id);
        Task DeleteActivity(int id);
        Task<EditActivityVM> GetActivity(int id);
        Task EditActivity(EditActivityVM model);
    }
}
