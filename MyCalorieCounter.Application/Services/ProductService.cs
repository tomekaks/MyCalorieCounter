using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Application.Interfaces.Repositories;
using MyCalorieCounter.Application.Interfaces.Services;
using MyCalorieCounter.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductFactory _productFactory;
        private readonly IUnitOfWork _unitOfWork;


        public ProductService(IProductFactory productFactory, IUnitOfWork unitOfWork)
        {
            _productFactory = productFactory;
            _unitOfWork = unitOfWork;
        }

        public void AddAProduct(string name, double cal, double pro, double carb, double fat)
        {
            ProductDto productDto = _productFactory.CreateProductDto(name, cal, pro, carb, fat);
            Product product = _productFactory.CreateProduct(productDto);

            _unitOfWork.Products.Add(product);
        }

        public ProductDto CreateNewProduct(string name, double cal, double pro, double carb, double fat)
        {
            return _productFactory.CreateProductDto(name, cal, pro, carb, fat);
        }

        public void DeleteAProduct(ProductDto productDto)
        {
            Product product = _productFactory.CreateProduct(productDto);
            _unitOfWork.Products.Delete(product);
        }

        public async Task<List<ProductDto>> GetProductList()
        {
            var productList = await _unitOfWork.Products.GetAll();
            return _productFactory.CreateProductDtoList(productList.ToList());
        }
    }
}
