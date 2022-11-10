using MediatR;
using MyCalorieCounter.Application.CQRS.Meal.Requests.Commands;
using MyCalorieCounter.Application.Exeptions;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Application.Interfaces.Repositories;
using MyCalorieCounter.Application.Interfaces.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.Meal.Handlers.Commands
{
    public class UpdateMealCommandHandler : IRequestHandler<UpdateMealCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMealFactory _mealFactory;
        private readonly IMealDtoValidator _mealDtoValidator;

        public UpdateMealCommandHandler(IUnitOfWork unitOfWork, IMealFactory mealFactory, IMealDtoValidator mealDtoValidator)
        {
            _unitOfWork = unitOfWork;
            _mealFactory = mealFactory;
            _mealDtoValidator = mealDtoValidator;
        }

        public async Task<Unit> Handle(UpdateMealCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _mealDtoValidator.Validate(request.MealDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationExeption(validationResult);
            }

            var meal = await _unitOfWork.Meals.Get(q => q.Id == request.MealDto.Id);
            meal = _mealFactory.MapToModel(meal, request.MealDto);

            _unitOfWork.Meals.Update(meal);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
