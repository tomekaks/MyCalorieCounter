using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.MyActivity.Requests.Commands
{
    public class DeleteMyActivityCommand : IRequest
    {
        public int Id { get; set; }
    }
}
