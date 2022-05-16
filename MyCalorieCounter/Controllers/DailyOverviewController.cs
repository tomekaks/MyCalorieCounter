using Microsoft.AspNetCore.Mvc;
using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Interfaces.Services;
using MyCalorieCounter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalorieCounter.Controllers
{
    public class DailyOverviewController : Controller
    {
        private readonly IDailySumService _dailySumService;
        private readonly ISettingService _settingService;

        public DailyOverviewController(IDailySumService dailySumService, ISettingService settingService)
        {
            _dailySumService = dailySumService;
            _settingService = settingService;
        }

        public async Task<IActionResult> Index()
        {
            var todaysMacros = await _dailySumService.GetTodaysMacros();
            var dailyGoals = await _settingService.GetDailyGoals();
            var model = new DailyOverviewVM(todaysMacros, dailyGoals);
            return View(model);
        }

        public async Task<IActionResult> UpdateGoals()
        {
            var dailyGoals = await _settingService.GetDailyGoals();
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
                var dailyGoals = new DailyGoals()
                {
                    Calories = model.DailyCaloriesGoal,
                    Proteins = model.DailyProteinsGoal,
                    Carbs = model.DailyCarbsGoal,
                    Fats = model.DailyFatsGoal
                };
                await _settingService.UpdateDailyGoals(dailyGoals);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }
    }
}
