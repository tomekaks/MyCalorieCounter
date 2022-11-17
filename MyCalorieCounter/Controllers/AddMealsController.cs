using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCalorieCounter.Application.CQRS.Meal.Requests.Commands;
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
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;


        public AddMealsController(IProductService productService, IDailySumService dailySumService, UserManager<ApplicationUser> userManager, IMealService mealService, IMapper mapper, IMediator mediator)
        {
            _productService = productService;
            _dailySumService = dailySumService;
            _userManager = userManager;
            _mealService = mealService;
            _mapper = mapper;
            _mediator = mediator;
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
            var model = _mapper.Map<AddFoodVM>(product);
            model.ProductId = id;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFood(AddFoodVM model, int productId)
        {
            
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var userId = await GetUsersId();
                //var product = await _productService.GetProduct(productId);
                //var dailySum = await _dailySumService.GetDailySum(userId);

                //dailySum.Calories += (product.Calories * model.Weight) / 100;
                //dailySum.Proteins += (product.Proteins * model.Weight) / 100;
                //dailySum.Carbs += (product.Carbs * model.Weight) / 100;
                //dailySum.Fats += (product.Fats * model.Weight) / 100;

                //await _dailySumService.UpdateDailySum(dailySum);
                //dailySum = await _dailySumService.GetDailySum(userId);

                var meal = _mapper.Map<MealDto>(model);
                meal.UserId = userId;

                //await _mealService.AddMeal(meal);

                await _mediator.Send(new CreateMealCommand { MealDto = meal });

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
