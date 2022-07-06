using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCalorieCounter.Application.Interfaces.Services;
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

        public ActivitiesController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        public async Task<IActionResult> Index()
        {
            var exercises = await _exerciseService.GetAllExercises();
            var model = new ActivitiesVM();
            model.Exercises = exercises;
            return View(model);
        }
    }
}
