using MediatR;
using MyCalorieCounter.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.MyActivity.Requests.Queries
{
    public class GetMyActivityRequest : IRequest<MyActivityDto>
    {
        public int Id { get; set; }
    }
}
