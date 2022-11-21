using MediatR;
using MyCalorieCounter.Application.CQRS.DailyGoal.Requests.Commands;
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

namespace MyCalorieCounter.Application.CQRS.DailyGoal.Handlers.Commands
{
    public class UpdateDailyGoalCommandHandler : IRequestHandler<UpdateDailyGoalCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDailyGoalFactory _dailyGoalFactory;
        private readonly IDailyGoalDtoValidator _dailyGoalDtoValidator;

        public UpdateDailyGoalCommandHandler(IUnitOfWork unitOfWork, IDailyGoalFactory dailyGoalFactory, IDailyGoalDtoValidator dailyGoalDtoValidator)
        {
            _unitOfWork = unitOfWork;
            _dailyGoalFactory = dailyGoalFactory;
            _dailyGoalDtoValidator = dailyGoalDtoValidator;
        }

        public async Task<Unit> Handle(UpdateDailyGoalCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _dailyGoalDtoValidator.Validate(request.DailyGoalDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationExeption(validationResult);
            }

            var dailyGoal = await _unitOfWork.DailyGoals.GetById(request.DailyGoalDto.UserId);
            dailyGoal = _dailyGoalFactory.MapToModel(dailyGoal, request.DailyGoalDto);

            _unitOfWork.DailyGoals.Update(dailyGoal);
            await _unitOfWork.Save();

            return Unit.Value;
        }

        
    }
}
