using MediatR;
using MyCalorieCounter.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.MyActivity.Requests.Commands
{
    public class UpdateMyActivityCommand : IRequest
    {
        public MyActivityDto MyActivityDto { get; set; }
    }
}
