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
    public class GetMealListRequestHandler : IRequestHandler<GetMealListRequest, List<MealDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMealFactory _mealFactory;

        public GetMealListRequestHandler(IUnitOfWork unitOfWork, IMealFactory mealFactory)
        {
            _unitOfWork = unitOfWork;
            _mealFactory = mealFactory;
        }

        public async Task<List<MealDto>> Handle(GetMealListRequest request, CancellationToken cancellationToken)
        {
            var mealList = await _unitOfWork.Meals.GetAll(q => q.DailySumId == request.DailySumId, includeProperties: "Product");
            return _mealFactory.CreateMealDtoList(mealList.ToList());
        }
    }
}
