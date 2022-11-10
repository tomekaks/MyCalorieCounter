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

            _unitOfWork.Meals.Delete(meal);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
