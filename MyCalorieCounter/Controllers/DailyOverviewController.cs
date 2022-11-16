using AutoMapper;
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
        private readonly IMapper _mapper;


        public DailyOverviewController(IDailySumService dailySumService, IDailyGoalService dailyGoalService,
                                       UserManager<ApplicationUser> userManager, IMealService mealService, IMyActivityService myActivityService, IMapper mapper)
        {
            _dailySumService = dailySumService;
            _dailyGoalService = dailyGoalService;
            _userManager = userManager;
            _mealService = mealService;
            _myActivityService = myActivityService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var currentUsersId = await GetUsersId();
            var dailySum = await _dailySumService.GetDailySum(currentUsersId);
            var dailyGoal = await _dailyGoalService.GetDailyGoal(currentUsersId);
            var todaysMeals = await _mealService.GetTodaysMeals(currentUsersId, dailySum.Date);
            var activities = await _myActivityService.GetTodaysActivities(dailySum.Id);
            var model = new DailyOverviewVM(dailySum, dailyGoal, todaysMeals, activities);
            return View(model);
        }

        public async Task<IActionResult> UpdateGoals()
        {
            var currentUsersId = await GetUsersId();
            var dailyGoal = await _dailyGoalService.GetDailyGoal(currentUsersId);
            var model = _mapper.Map<DailyGoalVM>(dailyGoal);
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateGoals(DailyGoalVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
               
                var dailyGoalDto = _mapper.Map<DailyGoalDto>(model);
                dailyGoalDto.UserId = await GetUsersId();
                await _dailyGoalService.UpdateDailyGoal(dailyGoalDto);

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
            var model = _mapper.Map<RemoveMealVM>(meal);

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
                var dailySum = await _dailySumService.GetDailySum(userId);
                var meal = await _mealService.GetMeal(id);
                dailySum.UserId = userId;
                dailySum.Calories -= meal.Calories;
                dailySum.Proteins -= meal.Proteins;
                dailySum.Carbs -= meal.Carbs;
                dailySum.Fats -= meal.Fats;
                await _dailySumService.BeginNewOrUpdateDailySum(dailySum);
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
            var model = _mapper.Map<EditMealVM>(meal);

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
                var dailySum = await _dailySumService.GetDailySum(userId);
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

                await _mealService.UpdateMeal(meal);
                await _dailySumService.BeginNewOrUpdateDailySum(dailySum);

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
            var model = _mapper.Map<RemoveActivityVM>(activity);

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
                var dailySum = await _dailySumService.GetDailySum(userId);
                var activity = await _myActivityService.GetMyActivity(id);

                dailySum.CaloriesBurned -= activity.CaloriesBurned;
                await _dailySumService.BeginNewOrUpdateDailySum(dailySum);

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
            var model = _mapper.Map<EditActivityVM>(activity);

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditActivity(EditActivityVM model, int id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var userId = await GetUsersId();
                var dailySum = await _dailySumService.GetDailySum(userId);
                var activity = await _myActivityService.GetMyActivity(id);

                dailySum.CaloriesBurned -= activity.CaloriesBurned;

                activity.Minutes = model.Minutes;
                activity.CaloriesBurned = activity.Exercise.CaloriesPerHour * activity.Minutes / 60;

                dailySum.CaloriesBurned += activity.CaloriesBurned;
                await _dailySumService.BeginNewOrUpdateDailySum(dailySum);

                await _myActivityService.UpdateActivity(activity);

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
