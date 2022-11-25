using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCalorieCounter.Interefaces.Services;
using MyCalorieCounter.Models;
using System.Threading.Tasks;

namespace MyCalorieCounter.Controllers
{
    [Authorize]
    public class ManageProductsController : Controller
    {
        private readonly IManageProductsService _manageProductsService;

        public ManageProductsController(IManageProductsService manageProductsService)
        {
            _manageProductsService = manageProductsService;
        }


        public async Task<IActionResult> Index()
        {
            var model = await _manageProductsService.GetProductList();
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

                await _manageProductsService.AddProduct(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _manageProductsService.DeleteProduct(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _manageProductsService.GetProduct(id);

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

                await _manageProductsService.EditProduct(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }
    }
}
