using AutoMapper;
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
    public class ManageExercisesController : Controller
    {
        private readonly IExerciseService _exerciseService;
        private readonly IMapper _mapper;

        public ManageExercisesController(IExerciseService exerciseService, IMapper mapper)
        {
            _exerciseService = exerciseService;
            _mapper = mapper;
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

                var exercise = _mapper.Map<ExerciseDto>(model);
                await _exerciseService.AddNewExercise(exercise);

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
            var model = _mapper.Map<ExerciseVM>(exercise);
            
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
            var model = _mapper.Map<ExerciseVM>(exercise);

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditExercise(ExerciseVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var exercise = _mapper.Map<ExerciseDto>(model);
                
                await _exerciseService.UpdateExercise(exercise);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }
    }
}
