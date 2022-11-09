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
    public class GetProductRequestHandler : IRequestHandler<GetProductRequest, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductFactory _productFactory;

        public GetProductRequestHandler(IUnitOfWork unitOfWork, IProductFactory productFactory)
        {
            _unitOfWork = unitOfWork;
            _productFactory = productFactory;
        }
        public async Task<ProductDto> Handle(GetProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.Get(q => q.Id == request.Id);
            return _productFactory.CreateProductDto(product);
        }
    }
}
