using Microsoft.AspNetCore.Mvc;
using MyCalorieCounter.Application.Interfaces.Services;
using MyCalorieCounter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalorieCounter.Controllers
{
    public class ManageExercisesController : Controller
    {
        private readonly IExerciseService _exerciseService;

        public ManageExercisesController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        public async Task<IActionResult> Index()
        {
            var exercises = await _exerciseService.GetAllExercises();
            var model = new ActivitiesVM()
            {
                Exercises = exercises
            };
            return View(model);
        }
        public IActionResult AddExercise()
        {
            var model = new ExerciseVM();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddExercise(ExerciseVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                await _exerciseService.AddNewExercise(model.Name, model.CaloriesPerHour);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var exercise = await _exerciseService.GetExercise(id);
            var model = new ExerciseVM()
            {
                Id = id,
                Name = exercise.Name,
                CaloriesPerHour = exercise.CaloriesPerHour
            };
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            try
            {
                await _exerciseService.DeleteExercise(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var exercise = await _exerciseService.GetExercise(id);
            var model = new ExerciseVM()
            {
                Id = exercise.Id,
                Name = exercise.Name,
                CaloriesPerHour = exercise.CaloriesPerHour
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditExercise(ExerciseVM model, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var exercise = await _exerciseService.GetExercise(id);
                exercise.Name = model.Name;
                exercise.CaloriesPerHour = model.CaloriesPerHour;
                await _exerciseService.UpdateExercise(exercise, id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }
    }
}
