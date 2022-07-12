using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCalorieCounter.Application.Interfaces.Services;
using MyCalorieCounter.Core.Data;
using MyCalorieCounter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalorieCounter.Controllers
{
    [Authorize]
    public class ActivitiesController : Controller
    {
        private readonly IExerciseService _exerciseService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDailySumService _dailySumService;
        private readonly IMyActivityService _myActivityService;

        public ActivitiesController(IExerciseService exerciseService, UserManager<ApplicationUser> userManager, IDailySumService dailySumService, IMyActivityService myActivityService)
        {
            _exerciseService = exerciseService;
            _userManager = userManager;
            _dailySumService = dailySumService;
            _myActivityService = myActivityService;
        }

        public async Task<IActionResult> Index()
        {
            var exercises = await _exerciseService.GetAllExercises();
            var model = new ActivitiesVM
            {
                Exercises = exercises
            };
            return View(model);
        }

        public async Task<IActionResult> Add(int id)
        {
            var exercise = await _exerciseService.GetExercise(id);
            var model = new AddActivityVM()
            {
                ExerciseId = id,
                Name = exercise.Name,
                CaloriesPerHour = exercise.CaloriesPerHour
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddActivity(AddActivityVM model, int exerciseId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var userId = await GetUsersId();
                var exercise = await _exerciseService.GetExercise(exerciseId);
                var dailySum = await _dailySumService.GetTodaysMacros(userId);
                var calories = (exercise.CaloriesPerHour * model.Minutes) / 60;
                await _myActivityService.AddActivity(userId, exerciseId, model.Minutes, calories, dailySum.Id);

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
