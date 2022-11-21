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
    public class UpdateMyActivityCommandHandler : IRequestHandler<UpdateMyActivityCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyActivityFactory _myActivityFactory;
        private readonly IMyActivityDtoValidator _myActivityDtoValidator;

        public UpdateMyActivityCommandHandler(IUnitOfWork unitOfWork, IMyActivityFactory myActivityFactory, IMyActivityDtoValidator myActivityDtoValidator)
        {
            _unitOfWork = unitOfWork;
            _myActivityFactory = myActivityFactory;
            _myActivityDtoValidator = myActivityDtoValidator;
        }

        public async Task<Unit> Handle(UpdateMyActivityCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _myActivityDtoValidator.Validate(request.MyActivityDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationExeption(validationResult);
            }

            var myActivity = await _unitOfWork.MyActivities.Get(q => q.Id == request.MyActivityDto.Id);
            var dailySum = await _unitOfWork.DailySums.Get(d => d.Id == myActivity.DailySumId);

            dailySum.CaloriesBurned -= myActivity.CaloriesBurned;

            myActivity = _myActivityFactory.MapToModel(myActivity, request.MyActivityDto);
            myActivity.CaloriesBurned = myActivity.Exercise.CaloriesPerHour * myActivity.Minutes / 60;

            dailySum.CaloriesBurned += myActivity.CaloriesBurned;

            _unitOfWork.DailySums.Update(dailySum);
            _unitOfWork.MyActivities.Update(myActivity);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
