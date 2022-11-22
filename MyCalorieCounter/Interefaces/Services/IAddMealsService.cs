using MyCalorieCounter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalorieCounter.Interefaces.Services
{
    public interface IAddMealsService
    {
        Task<AddMealListVM> GetMealList();
        Task<AddMealVM> GetMeal(int id);
        Task AddMeal(AddMealVM model, string userId);
    }
}
