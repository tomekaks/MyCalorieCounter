using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ManageProductsController : Controller
    {
        private readonly IProductService _productService;

        public ManageProductsController(IProductService productService)
        {
            _productService = productService;
        }


        public async Task<IActionResult> Index()
        {
            var productList = await _productService.GetProductList();
            var model = new AddMealsVM();
            model.Products = productList;
            return View(model);
        }

        public IActionResult AddProduct()
        {
            var model = new ProductVM();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(ProductVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
 
                await _productService.AddAProduct(model.Name, model.Calories, model.Proteins, model.Carbs, model.Fats);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProduct(id);
            var model = new ProductVM()
            {
                Id = product.Id,
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
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteAProduct(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
