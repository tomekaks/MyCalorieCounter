using MediatR;
using MyCalorieCounter.Application.CQRS.MyActivity.Requests.Commands;
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

namespace MyCalorieCounter.Application.CQRS.MyActivity.Handlers.Commands
{
    class CreateMyActivityCommandHandler : IRequestHandler<CreateMyActivityCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyActivityFactory _myActivityFactory;
        private readonly IMyActivityDtoValidator _myActivityDtoValidator;

        public CreateMyActivityCommandHandler(IUnitOfWork unitOfWork, IMyActivityFactory myActivityFactory, IMyActivityDtoValidator myActivityDtoValidator)
        {
            _unitOfWork = unitOfWork;
            _myActivityFactory = myActivityFactory;
            _myActivityDtoValidator = myActivityDtoValidator;
        }

        public async Task<Unit> Handle(CreateMyActivityCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _myActivityDtoValidator.Validate(request.MyActivityDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationExeption(validationResult);
            }

            var todaysDate = GetTodaysDate();

            var myActivity = _myActivityFactory.CreateMyActivity(request.MyActivityDto);

            var exercise = await _unitOfWork.Exercises.Get(q => q.Id == request.MyActivityDto.ExerciseId);
            var dailySum = await _unitOfWork.DailySums.Get(q => q.UserId == request.MyActivityDto.UserId
                                                             && q.Date == todaysDate);

            var caloriesBurned = (exercise.CaloriesPerHour * request.MyActivityDto.Minutes) / 60;
            dailySum.CaloriesBurned += caloriesBurned;

            myActivity.DailySumId = dailySum.Id;
            myActivity.CaloriesBurned = caloriesBurned;

            _unitOfWork.DailySums.Update(dailySum);
            await _unitOfWork.MyActivities.Add(myActivity);
            await _unitOfWork.Save();

            return Unit.Value;
        }

        private string GetTodaysDate()
        {
            return DateTime.Today.ToString("d");
        }
    }
}
