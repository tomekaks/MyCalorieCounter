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
        private readonly ISettingService _settingService;
        private readonly IDailyGoalService _dailyGoalService;
        private string _currentUsersId;
        private readonly UserManager<ApplicationUser> _userManager;

        public DailyOverviewController(IDailySumService dailySumService, ISettingService settingService, IDailyGoalService dailyGoalService, UserManager<ApplicationUser> userManager)
        {
            _dailySumService = dailySumService;
            _settingService = settingService;
            _dailyGoalService = dailyGoalService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            await GetUsersId();
            var todaysMacros = await _dailySumService.GetTodaysMacros();
            //var dailyGoals = await _settingService.GetDailyGoals();
            var dailyGoals = await _dailyGoalService.GetDailyGoal(_currentUsersId);
            var model = new DailyOverviewVM(todaysMacros, dailyGoals);
            return View(model);
        }

        public async Task<IActionResult> UpdateGoals()
        {
            var dailyGoals = await _dailyGoalService.GetDailyGoal(_currentUsersId);
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
                //var dailyGoals = new DailyGoalDto()
                //{
                //    Calories = model.DailyCaloriesGoal,
                //    Proteins = model.DailyProteinsGoal,
                //    Carbs = model.DailyCarbsGoal,
                //    Fats = model.DailyFatsGoal
                //};
                //await _settingService.UpdateDailyGoals(dailyGoals);
                await _dailyGoalService.UpdateDailyGoal(_currentUsersId, 
                                                        model.DailyCaloriesGoal, model.DailyProteinsGoal, 
                                                        model.DailyCarbsGoal, model.DailyFatsGoal);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }
        private async Task GetUsersId()
        {
            var user = await _userManager.GetUserAsync(User);
            _currentUsersId = user.Id;
            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //return claim.Value;
            //return user.Id;
        }
    }
}
