using MediatR;
using MyCalorieCounter.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.Product.Requsts.Queries
{
    public class GetProductRequest : IRequest<ProductDto>
    {
        public int Id { get; set; }
    }
}
