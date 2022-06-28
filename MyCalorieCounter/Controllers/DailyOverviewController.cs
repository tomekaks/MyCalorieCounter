﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMealService _mealService;

        public DailyOverviewController(IDailySumService dailySumService, ISettingService settingService, IDailyGoalService dailyGoalService, 
                                       UserManager<ApplicationUser> userManager, IMealService mealService)
        {
            _dailySumService = dailySumService;
            _settingService = settingService;
            _dailyGoalService = dailyGoalService;
            _userManager = userManager;
            _mealService = mealService;
        }

        public async Task<IActionResult> Index()
        {
            var currentUsersId = await GetUsersId();
            var todaysMacros = await _dailySumService.GetTodaysMacros(currentUsersId);
            var dailyGoals = await _dailyGoalService.GetDailyGoal(currentUsersId);
            var todaysMeals = await _mealService.GetTodaysMeals(currentUsersId, todaysMacros.Date);
            var model = new DailyOverviewVM(todaysMacros, dailyGoals, todaysMeals);
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
