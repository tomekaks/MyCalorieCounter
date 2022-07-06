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
            //var product = await _exerciseService.GetProduct(id);
            var model = new ProductVM();
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    Calories = product.Calories,
            //    Proteins = product.Proteins,
            //    Carbs = product.Carbs,
            //    Fats = product.Fats
            //};
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                //await _exerciseService.DeleteAProduct(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            //var product = await _exerciseService.GetProduct(id);
            var model = new ProductVM();
            //{
            //    Id = id,
            //    Name = product.Name,
            //    Calories = product.Calories,
            //    Proteins = product.Proteins,
            //    Carbs = product.Carbs,
            //    Fats = product.Fats
            //};
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductVM model, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                //var product = await _exerciseService.GetProduct(id);
                //product.Name = model.Name;
                //product.Calories = model.Calories;
                //product.Proteins = model.Proteins;
                //product.Carbs = model.Carbs;
                //product.Fats = model.Fats;
                //await _exerciseService.UpdateProduct(product);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }
    }
}
