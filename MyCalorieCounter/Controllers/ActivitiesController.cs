using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Core.Data;
using MyCalorieCounter.Interefaces.Services;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IActivitiesService _activitiesService;

        public ActivitiesController(UserManager<ApplicationUser> userManager, IActivitiesService activitiesService)
        {
            _userManager = userManager;
            _activitiesService = activitiesService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _activitiesService.GetActivityList();
            return View(model);
        }

        public async Task<IActionResult> Add(int id)
        {
            var model = await _activitiesService.GetActivity(id);
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

                await _activitiesService.AddActivity(model, userId);

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
