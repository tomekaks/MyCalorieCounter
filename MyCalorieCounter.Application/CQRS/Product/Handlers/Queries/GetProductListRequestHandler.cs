using MediatR;
using MyCalorieCounter.Application.CQRS.Product.Requsts.Queries;
using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.Product.Handlers.Queries
{
    public class GetProductListRequestHandler : IRequestHandler<GetProductListRequest, List<ProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductFactory _productFactory;

        public GetProductListRequestHandler(IUnitOfWork unitOfWork, IProductFactory productFactory)
        {
            _unitOfWork = unitOfWork;
            _productFactory = productFactory;
        }
        public async Task<List<ProductDto>> Handle(GetProductListRequest request, CancellationToken cancellationToken)
        {
            var productList = await _unitOfWork.Products.GetAll();
            return _productFactory.CreateProductDtoList(productList.ToList());
        }
    }
}
