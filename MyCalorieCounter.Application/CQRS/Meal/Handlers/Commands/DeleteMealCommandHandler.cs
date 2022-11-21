using MediatR;
using MyCalorieCounter.Application.CQRS.Meal.Requests.Commands;
using MyCalorieCounter.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.Meal.Handlers.Commands
{
    public class DeleteMealCommandHandler : IRequestHandler<DeleteMealCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMealCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteMealCommand request, CancellationToken cancellationToken)
        {
            var meal = await _unitOfWork.Meals.Get(q => q.Id == request.Id);
            var dailySum = await _unitOfWork.DailySums.Get(d => d.Id == meal.DailySumId);

            dailySum.Calories -= meal.Calories;
            dailySum.Proteins -= meal.Proteins;
            dailySum.Carbs -= meal.Carbs;
            dailySum.Fats -= meal.Fats;

            _unitOfWork.DailySums.Update(dailySum);
            _unitOfWork.Meals.Delete(meal);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
