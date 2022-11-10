using MediatR;
using MyCalorieCounter.Application.CQRS.Meal.Requests.Queries;
using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.Meal.Handlers.Queries
{
    public class GetMealRequestHandler : IRequestHandler<GetMealRequest, MealDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMealFactory _mealFactory;

        public GetMealRequestHandler(IUnitOfWork unitOfWork, IMealFactory mealFactory)
        {
            _unitOfWork = unitOfWork;
            _mealFactory = mealFactory;
        }

        public async Task<MealDto> Handle(GetMealRequest request, CancellationToken cancellationToken)
        {
            var meal = await _unitOfWork.Meals.Get(q => q.Id == request.Id, includeProperties: "Product");
            return _mealFactory.CreateMealDto(meal);
        }
    }
}
