using MyCalorieCounter.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetProductList();
        ProductDto CreateNewProduct(string name, double cal, double pro, double carb, double fat);
        Task AddAProduct(string name, double cal, double pro, double carb, double fat);
        Task DeleteAProduct(int id);
        Task<ProductDto> GetProduct(int id);
        Task UpdateProduct(ProductDto productDto);
    }
}
