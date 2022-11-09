using MediatR;
using MyCalorieCounter.Application.CQRS.Product.Requsts.Commands;
using MyCalorieCounter.Application.Exeptions;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Application.Interfaces.Repositories;
using MyCalorieCounter.Application.Interfaces.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.Product.Handlers.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductFactory _productFactory;
        private readonly IProductDtoValidator _productDtoValidator;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IProductFactory productFactory, IProductDtoValidator productDtoValidator)
        {
            _unitOfWork = unitOfWork;
            _productFactory = productFactory;
            _productDtoValidator = productDtoValidator;
        }
        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _productDtoValidator.Validate(request.ProductDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationExeption(validationResult);
            }

            var product = _productFactory.CreateProduct(request.ProductDto);

            await _unitOfWork.Products.Add(product);
            await _unitOfWork.Save();
             
            return Unit.Value;
        }

    }
}
