using AutoMapper;
using MediatR;
using MyCalorieCounter.Application.CQRS.Product.Requsts.Commands;
using MyCalorieCounter.Application.CQRS.Product.Requsts.Queries;
using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Interefaces.Services;
using MyCalorieCounter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalorieCounter.Services
{
    public class ManageProductsService : IManageProductsService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ManageProductsService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task AddProduct(ProductVM model)
        {
            var productDto = _mapper.Map<ProductDto>(model);

            await _mediator.Send(new CreateProductCommand { ProductDto = productDto });
        }

        public async Task DeleteProduct(int id)
        {
            await _mediator.Send(new DeleteProductCommand { Id = id });
        }

        public async Task EditProduct(ProductVM model)
        {
            var productDto = _mapper.Map<ProductDto>(model);

            await _mediator.Send(new UpdateProductCommand { ProductDto = productDto });
        }

        public async Task<ProductVM> GetProduct(int id)
        {
            var productDto = await _mediator.Send(new GetProductRequest { Id = id });

            return _mapper.Map<ProductVM>(productDto);
        }

        public async Task<AddMealsVM> GetProductList()
        {
            var productList = await _mediator.Send(new GetProductListRequest());

            return new AddMealsVM()
            {
                Products = productList
            };
        }
    }
}
