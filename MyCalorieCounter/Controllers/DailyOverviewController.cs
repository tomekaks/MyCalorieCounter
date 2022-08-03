using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Interfaces.Services;
using MyCalorieCounter.Core.Data;
using MyCalorieCounter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyCalorieCounter.Controllers
{
    [Authorize]
    public class DailyOverviewController : Controller
    {
        private readonly IDailySumService _dailySumService;
        private readonly IDailyGoalService _dailyGoalService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMealService _mealService;
        private readonly IMyActivityService _myActivityService;


        public DailyOverviewController(IDailySumService dailySumService, IDailyGoalService dailyGoalService,
                                       UserManager<ApplicationUser> userManager, IMealService mealService, IMyActivityService myActivityService)
        {
            _dailySumService = dailySumService;
            _dailyGoalService = dailyGoalService;
            _userManager = userManager;
            _mealService = mealService;
            _myActivityService = myActivityService;
        }

        public async Task<IActionResult> Index()
        {
            var currentUsersId = await GetUsersId();
            var todaysMacros = await _dailySumService.GetTodaysMacros(currentUsersId);
            var dailyGoals = await _dailyGoalService.GetDailyGoal(currentUsersId);
            var todaysMeals = await _mealService.GetTodaysMeals(currentUsersId, todaysMacros.Date);
            var activities = await _myActivityService.GetTodaysActivities(todaysMacros.Id);
            var model = new DailyOverviewVM(todaysMacros, dailyGoals, todaysMeals, activities);
            return View(model);
        }

        public async Task<IActionResult> UpdateGoals()
        {
            var currentUsersId = await GetUsersId();
            var dailyGoals = await _dailyGoalService.GetDailyGoal(currentUsersId);
            var model = new DailyGoalsVM()
            {
                DailyCaloriesGoal = dailyGoals.Calories,
                DailyProteinsGoal = dailyGoals.Proteins,
                DailyCarbsGoal = dailyGoals.Carbs,
                DailyFatsGoal = dailyGoals.Fats
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateGoals(DailyGoalsVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
               
                var currentUsersId = await GetUsersId();
                await _dailyGoalService.UpdateDailyGoal(currentUsersId, 
                                                        model.DailyCaloriesGoal, model.DailyProteinsGoal, 
                                                        model.DailyCarbsGoal, model.DailyFatsGoal);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }
        public async Task<IActionResult> RemoveMeal(int id)
        {
            var meal = await _mealService.GetMeal(id);
            var model = new RemoveMealVM()
            {
                Id = id,
                Name = meal.Product.Name,
                Calories = meal.Calories,
                Proteins = meal.Proteins,
                Carbs = meal.Carbs,
                Fats = meal.Fats,
                Weight = meal.Weight
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveMeal(RemoveMealVM model, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var userId = await GetUsersId();
                var dailySum = await _dailySumService.GetTodaysMacros(userId);
                var meal = await _mealService.GetMeal(id);
                dailySum.UserId = userId;
                dailySum.Calories -= meal.Calories;
                dailySum.Proteins -= meal.Proteins;
                dailySum.Carbs -= meal.Carbs;
                dailySum.Fats -= meal.Fats;
                await _dailySumService.BeginNewOrUpdateTodaysMacros(dailySum);
                await _mealService.DeleteMeal(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        public async Task<IActionResult> EditMeal(int id)
        {
            var meal = await _mealService.GetMeal(id);
            var model = new EditMealVM()
            {
                Id = id,
                Name = meal.Product.Name,
                Calories = meal.Calories,
                Proteins = meal.Proteins,
                Carbs = meal.Carbs,
                Fats = meal.Fats,
                Weight = meal.Weight
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMeal(EditMealVM model, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var userId = await GetUsersId();
                var dailySum = await _dailySumService.GetTodaysMacros(userId);
                var meal = await _mealService.GetMeal(id);
                dailySum.Calories -= meal.Calories;
                dailySum.Proteins -= meal.Proteins;
                dailySum.Carbs -= meal.Carbs;
                dailySum.Fats -= meal.Fats;

                meal.Weight = model.Weight;
                dailySum.Calories += meal.Calories;
                dailySum.Proteins += meal.Proteins;
                dailySum.Carbs += meal.Carbs;
                dailySum.Fats += meal.Fats;

                await _mealService.UpdateMeal(meal, id);
                await _dailySumService.BeginNewOrUpdateTodaysMacros(dailySum);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        public async Task<IActionResult> RemoveActivity(int id)
        {
            var activity = await _myActivityService.GetMyActivity(id);
            var model = new RemoveActivityVM()
            {
                Id = id,
                Name = activity.Exercise.Name,
                CaloriesBurned = activity.Calories,
                Minutes = activity.Minutes
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveActivity(RemoveActivityVM model, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var userId = await GetUsersId();
                var dailySum = await _dailySumService.GetTodaysMacros(userId);
                var activity = await _myActivityService.GetMyActivity(id);
                await _myActivityService.DeleteActivity(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        public async Task<IActionResult> EditActivity(int id)
        {
            var activity = await _myActivityService.GetMyActivity(id);
            var model = new EditActivityVM()
            {
                Id = id,
                Name = activity.Exercise.Name,
                CaloriesBurned = activity.Calories,
                Minutes = activity.Minutes
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditActivity(EditActivityVM model, int id)
        {
            //TODO doesn't update

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var userId = await GetUsersId();
                var dailySum = await _dailySumService.GetTodaysMacros(userId);
                var activity = await _myActivityService.GetMyActivity(id);
                activity.Minutes = model.Minutes;
                activity.Calories = activity.Exercise.CaloriesPerHour * activity.Minutes / 60;
                await _myActivityService.UpdateActivity(activity, id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        private async Task<string> GetUsersId()
        {
            var user = await _userManager.GetUserAsync(User);

            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //return claim.Value;
            return user.Id;
        }
    }
}
