using MediatR;
using MyCalorieCounter.Application.CQRS.Product.Requsts.Commands;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.Product.Handlers.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductFactory _productFactory;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IProductFactory productFactory)
        {
            _unitOfWork = unitOfWork;
            _productFactory = productFactory;
        }
        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _productFactory.CreateProduct(request.ProductDto);
            await _unitOfWork.Products.Update(product);
            await _unitOfWork.Save();
            return Unit.Value;
        }
    }
}
