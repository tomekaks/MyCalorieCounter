using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Exeptions;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Application.Interfaces.Repositories;
using MyCalorieCounter.Application.Interfaces.Services;
using MyCalorieCounter.Application.Interfaces.Validators;
using MyCalorieCounter.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Services
{
    [Obsolete]
    public class ProductService : IProductService
    {
        private readonly IProductFactory _productFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductDtoValidator _productDtoValidator;

        public ProductService(IProductFactory productFactory, IUnitOfWork unitOfWork, IProductDtoValidator productDtoValidator)
        {
            _productFactory = productFactory;
            _unitOfWork = unitOfWork;
            _productDtoValidator = productDtoValidator;
        }

        public async Task AddAProduct(ProductDto productDto)
        {
            var validationResult = _productDtoValidator.Validate(productDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationExeption(validationResult);
            }

            var product = _productFactory.CreateProduct(productDto);

            await _unitOfWork.Products.Add(product);
            await _unitOfWork.Save();
        }

        public async Task DeleteAProduct(int id)
        {
            var product = await _unitOfWork.Products.Get(q => q.Id == id);
            _unitOfWork.Products.Delete(product);
            await _unitOfWork.Save();
        }

        public async Task<ProductDto> GetProduct(int id)
        {
            var product = await _unitOfWork.Products.Get(q => q.Id == id);
            return _productFactory.CreateProductDto(product);
        }

        public async Task<List<ProductDto>> GetProductList()
        {
            var productList = await _unitOfWork.Products.GetAll();
            return _productFactory.CreateProductDtoList(productList.ToList());
        }

        public async Task UpdateProduct(ProductDto productDto)
        {
            var validationResult = _productDtoValidator.Validate(productDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationExeption(validationResult);
            }
            var product = await _unitOfWork.Products.Get(q => q.Id == productDto.Id);
            product = _productFactory.MapToModel(product, productDto);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.Save();
        }
    }
}
