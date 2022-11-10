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
    public class CreateMealCommandHandler : IRequestHandler<CreateMealCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMealFactory _mealFactory;
        private readonly IMealDtoValidator _mealDtoValidator;

        public CreateMealCommandHandler(IUnitOfWork unitOfWork, IMealFactory mealFactory, IMealDtoValidator mealDtoValidator)
        {
            _unitOfWork = unitOfWork;
            _mealFactory = mealFactory;
            _mealDtoValidator = mealDtoValidator;
        }

        public async Task<Unit> Handle(CreateMealCommand request, CancellationToken cancellationToken)
        {
            var validationRestult = _mealDtoValidator.Validate(request.MealDto);
            if (!validationRestult.IsValid)
            {
                throw new ValidationExeption(validationRestult);
            }

            var meal = _mealFactory.CreateMeal(request.MealDto);

            await _unitOfWork.Meals.Add(meal);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
