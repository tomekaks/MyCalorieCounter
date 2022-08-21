using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Interfaces.Services;
using MyCalorieCounter.Core.Data;
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
        private readonly IProductService _productService;
        private readonly IDailySumService _dailySumService;
        private readonly IMealService _mealService;
        private readonly UserManager<ApplicationUser> _userManager;


        public AddMealsController(IProductService productService, IDailySumService dailySumService, UserManager<ApplicationUser> userManager, IMealService mealService)
        {
            _productService = productService;
            _dailySumService = dailySumService;
            _userManager = userManager;
            _mealService = mealService;
        }

        public async Task<IActionResult> Index()
        {
            var productList = await _productService.GetProductList();
            var model = new AddMealsVM();
            model.Products = productList;
            return View(model);
        }

        public async Task<IActionResult> AddFood(int id)
        {
            var product = await _productService.GetProduct(id);
            var model = new AddFoodVM()
            {
                ProductId = id,
                Name = product.Name,
                Calories = product.Calories,
                Proteins = product.Proteins,
                Carbs = product.Carbs,
                Fats = product.Fats
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFood(AddFoodVM model, int productId)
        {
            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //meal.UserId = claim.Value;
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var userId = await GetUsersId();
                var product = await _productService.GetProduct(productId);
                var dailySum = await _dailySumService.GetDailySum(userId);

                dailySum.Calories += (product.Calories * model.Weight) / 100;
                dailySum.Proteins += (product.Proteins * model.Weight) / 100;
                dailySum.Carbs += (product.Carbs * model.Weight) / 100;
                dailySum.Fats += (product.Fats * model.Weight) / 100;

                await _dailySumService.BeginNewOrUpdateDailySum(dailySum);
                dailySum = await _dailySumService.GetDailySum(userId);
                
                var meal = new MealDto()
                {
                    DailySumId = dailySum.Id,
                    UserId = userId,
                    Date = dailySum.Date,
                    ProductId = productId,
                    Weight = model.Weight
                };
                await _mealService.AddMeal(meal);

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
