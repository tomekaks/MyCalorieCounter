using MediatR;
using MyCalorieCounter.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.Meal.Requests.Queries
{
    public class GetMealRequest : IRequest<MealDto>
    {
        public int Id { get; set; }
    }
}
