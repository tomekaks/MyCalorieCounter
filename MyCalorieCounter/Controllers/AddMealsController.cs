using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Interfaces.Services;
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


        public AddMealsController(IProductService productService, IDailySumService dailySumService)
        {
            _productService = productService;
            _dailySumService = dailySumService;
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
        public async Task<IActionResult> AddFood(AddFoodVM model, MealDto meal)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            meal.UserId = claim.Value;
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var dailySum = await _dailySumService.GetTodaysMacros();
                dailySum.Calories += model.Calories * (model.Weight / 100);
                dailySum.Proteins += model.Proteins * (model.Weight / 100);
                dailySum.Carbs += model.Carbs * (model.Weight / 100);
                dailySum.Fats += model.Fats * (model.Weight / 100);
                await _dailySumService.BeginNewOrUpdateTodaysMacros(dailySum);
               
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        public async Task<IActionResult> RemoveFood(int id)
        {
            var product = await _productService.GetProduct(id);
            var model = new AddFoodVM()
            {
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
        public async Task<IActionResult> RemoveFood(AddFoodVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var dailySum = await _dailySumService.GetTodaysMacros();
                dailySum.Calories -= model.Calories * (model.Weight / 100);
                dailySum.Proteins -= model.Proteins * (model.Weight / 100);
                dailySum.Carbs -= model.Carbs * (model.Weight / 100);
                dailySum.Fats -= model.Fats * (model.Weight / 100);
                await _dailySumService.BeginNewOrUpdateTodaysMacros(dailySum);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }
    }
}
