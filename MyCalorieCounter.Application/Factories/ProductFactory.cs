using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Factories
{
    public class ProductFactory : IProductFactory
    {
        public Product CreateProduct(ProductDto productDto)
        {
            return new Product()
            {
                Name = productDto.Name,
                Calories = productDto.Calories,
                Proteins = productDto.Proteins,
                Carbs = productDto.Carbs,
                Fats = productDto.Fats
            };
        }
        public ProductDto CreateProductDto(Product product)
        {
            return new ProductDto()
            {
                Name = product.Name,
                Calories = product.Calories,
                Proteins = product.Proteins,
                Carbs = product.Carbs,
                Fats = product.Fats,
            };
        }
        public ProductDto CreateProductDto(string name, double cal, double pro, double carb, double fat)
        {
            return new ProductDto()
            {
                Name = name,
                Calories = cal,
                Proteins = pro,
                Carbs = carb,
                Fats = fat,
            };
        }
        public List<ProductDto> CreateProductDtoList(List<Product> productList)
        {
            var products = new List<ProductDto>();
            foreach (var item in productList)
            {
               products.Add(CreateProductDto(item));
            }
            return products;
            //return productList.Select(p => CreateProductDto(p)).ToList();
        }
    }
}
