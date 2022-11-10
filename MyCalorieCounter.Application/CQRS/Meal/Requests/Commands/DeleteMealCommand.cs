using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.Meal.Requests.Commands
{
    public class DeleteMealCommand : IRequest
    {
        public int Id { get; set; }
    }
}
