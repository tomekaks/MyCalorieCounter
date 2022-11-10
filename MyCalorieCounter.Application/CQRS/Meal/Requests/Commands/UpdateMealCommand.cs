using MediatR;
using MyCalorieCounter.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.Meal.Requests.Commands
{
    public class UpdateMealCommand : IRequest
    {
        public MealDto MealDto{ get; set; }
    }
}
