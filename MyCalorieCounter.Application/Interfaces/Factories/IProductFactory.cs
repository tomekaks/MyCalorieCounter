using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Interfaces.Factories
{
    public interface IProductFactory
    {
        Product CreateProduct(ProductDto productDto);
        ProductDto CreateProductDto(Product product);
        ProductDto CreateProductDto(string name, double cal, double pro, double carb, double fat);
        List<ProductDto> CreateProductDtoList(List<Product> productList);
    }
}
