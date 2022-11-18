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

            var todaysDate = GetTodaysDate();

            var meal = _mealFactory.CreateMeal(request.MealDto);

            var product = await _unitOfWork.Products.Get(q => q.Id == request.MealDto.ProductId);
            var dailySum = await _unitOfWork.DailySums.Get(q => q.UserId == request.MealDto.UserId
                                                             && q.Date == todaysDate);

            meal.Calories = (product.Calories * meal.Weight) / 100;
            meal.Proteins = (product.Proteins * meal.Weight) / 100;
            meal.Carbs = (product.Carbs * meal.Weight) / 100;
            meal.Fats = (product.Fats * meal.Weight) / 100;
            meal.DailySumId = dailySum.Id;
            meal.Date = dailySum.Date;

            dailySum.Calories += meal.Calories;
            dailySum.Proteins += meal.Proteins;
            dailySum.Carbs += meal.Carbs;
            dailySum.Fats += meal.Fats;

            _unitOfWork.DailySums.Update(dailySum);
            await _unitOfWork.Meals.Add(meal);
            await _unitOfWork.Save();

            return Unit.Value;
        }
        private string GetTodaysDate()
        {
            return DateTime.Today.ToString("d");
        }
    }
}
