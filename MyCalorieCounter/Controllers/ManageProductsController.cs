using AutoMapper;
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
        private readonly IMapper _mapper;

        public ManageProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            var productList = await _productService.GetProductList();
            var model = new AddMealsVM
            {
                Products = productList
            };
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

                var productDto = _mapper.Map<ProductDto>(model);
                
                await _productService.AddAProduct(productDto);

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
            var model = _mapper.Map<ProductVM>(product);
            
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

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProduct(id);
            var model = _mapper.Map<ProductVM>(product);

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductVM model, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var product = _mapper.Map<ProductDto>(model);
                await _productService.UpdateProduct(product);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }
    }
}
