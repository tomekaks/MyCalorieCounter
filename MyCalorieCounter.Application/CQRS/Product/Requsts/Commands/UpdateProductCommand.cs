using MediatR;
using MyCalorieCounter.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.Product.Requsts.Commands
{
    public class UpdateProductCommand : IRequest
    {
        public ProductDto ProductDto { get; set; }
    }
}
