using Microsoft.AspNetCore.Mvc;
using MyCalorieCounter.Application.Interfaces.Services;
using MyCalorieCounter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalorieCounter.Controllers
{
    public class AddMealsController : Controller
    {
        private readonly IProductService _productService;

        public AddMealsController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var productList = await _productService.GetProductList();
            var model = new AddMealsVM();
            model.Products = productList;
            //foreach (var item in productList)
            //{
            //    products.Add(new ProductVM
            //    {
            //        Name = item.Name,
            //        Calories = item.Calories,
            //        Proteins = item.Proteins,
            //        Carbs = item.Carbs,
            //        Fats = item.Fats
            //    });
            //}
            return View(model);
        }
    }
}
