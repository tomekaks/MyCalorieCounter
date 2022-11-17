using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCalorieCounter.Application.CQRS.MyActivity.Requests.Commands;
using MyCalorieCounter.Application.Dto;
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
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ActivitiesController(IExerciseService exerciseService, UserManager<ApplicationUser> userManager, IDailySumService dailySumService, IMyActivityService myActivityService, IMapper mapper, IMediator mediator)
        {
            _exerciseService = exerciseService;
            _userManager = userManager;
            _dailySumService = dailySumService;
            _myActivityService = myActivityService;
            _mapper = mapper;
            _mediator = mediator;
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
                
                var myActivity = _mapper.Map<MyActivityDto>(model);
                myActivity.UserId = userId;
                
                await _myActivityService.AddActivity(myActivity);

                //await _mediator.Send(new CreateMyActivityCommand { MyActivityDto = myActivity });

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
