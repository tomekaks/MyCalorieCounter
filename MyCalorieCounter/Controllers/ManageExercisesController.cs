using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyCalorieCounter.Application.CQRS.Exercise.Requests.Commands;
using MyCalorieCounter.Application.CQRS.Exercise.Requests.Queries;
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
        private readonly IMediator _mediator;

        public ManageExercisesController(IExerciseService exerciseService, IMapper mapper, IMediator mediator)
        {
            _exerciseService = exerciseService;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            //var exercises = await _exerciseService.GetAllExercises();

            var exercises = await _mediator.Send(new GetExerciseListRequest());

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
                //await _exerciseService.AddNewExercise(exercise);

                await _mediator.Send(new CreateExerciseCommand { ExerciseDto = exercise });

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            //var exercise = await _exerciseService.GetExercise(id);

            var exerciseDto = await _mediator.Send(new GetExerciseRequest { Id = id });

            var model = _mapper.Map<ExerciseVM>(exerciseDto);
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            try
            {
                //await _exerciseService.DeleteExercise(id);

                await _mediator.Send(new DeleteExerciseCommand { Id = id });

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            //var exercise = await _exerciseService.GetExercise(id);

            var exerciseDto = await _mediator.Send(new GetExerciseRequest { Id = id });

            var model = _mapper.Map<ExerciseVM>(exerciseDto);

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

                //await _exerciseService.UpdateExercise(exercise);

                await _mediator.Send(new UpdateExerciseCommand { ExerciaseDto = exercise });

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }
    }
}
