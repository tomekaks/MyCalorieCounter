using MyCalorieCounter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalorieCounter.Interefaces.Services
{
    public interface IManageProductsService
    {
        Task<AddMealsVM> GetProductList();
        Task AddProduct(ProductVM model);
        Task DeleteProduct(int id);
        Task EditProduct(ProductVM model);
        Task<ProductVM> GetProduct(int id);
    }
}
