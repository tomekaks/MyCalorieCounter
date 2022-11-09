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
            var product = await _unitOfWork.Products.Get(q => q.Id == request.ProductDto.Id);
            product = _productFactory.MapToModel(product, request.ProductDto);

            _unitOfWork.Products.Update(product);
            await _unitOfWork.Save();
            return Unit.Value;
        }
    }
}
