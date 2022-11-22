using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCalorieCounter.Application.CQRS.Meal.Requests.Commands;
using MyCalorieCounter.Application.CQRS.Product.Requsts.Queries;
using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Interfaces.Services;
using MyCalorieCounter.Core.Data;
using MyCalorieCounter.Interefaces.Services;
using MyCalorieCounter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyCalorieCounter.Controllers
{
    [Authorize]
    public class AddMealsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAddMealsService _addMealsService;


        public AddMealsController(UserManager<ApplicationUser> userManager, IAddMealsService addMealsService)
        {
            _userManager = userManager;
            _addMealsService = addMealsService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _addMealsService.GetMealList();
            return View(model);
        }

        public async Task<IActionResult> AddMeal(int id)
        {
            var model = await _addMealsService.GetMeal(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMeal(AddMealVM model, int productId)
        {
            
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var userId = await GetUsersId();

                await _addMealsService.AddMeal(model, userId);

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
