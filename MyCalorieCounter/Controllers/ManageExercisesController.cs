using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyCalorieCounter.Application.CQRS.Exercise.Requests.Commands;
using MyCalorieCounter.Application.CQRS.Exercise.Requests.Queries;
using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Interefaces.Services;
using MyCalorieCounter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalorieCounter.Controllers
{
    public class ManageExercisesController : Controller
    {
        private readonly IManageExercisesService _manageExercisesService;

        public ManageExercisesController(IManageExercisesService manageExercisesService)
        {
            _manageExercisesService = manageExercisesService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _manageExercisesService.GetExerciseList();
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

                await _manageExercisesService.AddExercise(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            try
            {
                await _manageExercisesService.DeleteExercise(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _manageExercisesService.GetExercise(id);
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

                await _manageExercisesService.EditExercise(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }
    }
}
