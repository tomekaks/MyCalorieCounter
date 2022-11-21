using MediatR;
using MyCalorieCounter.Application.CQRS.DailyGoal.Requests.Commands;
using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.DailyGoal.Handlers.Commands
{
    public class CreateDailyGoalCommandHandler : IRequestHandler<CreateDailyGoalCommand, DailyGoalDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDailyGoalFactory _dailyGoalFactory;

        public CreateDailyGoalCommandHandler(IUnitOfWork unitOfWork, IDailyGoalFactory dailyGoalFactory = null)
        {
            _unitOfWork = unitOfWork;
            _dailyGoalFactory = dailyGoalFactory;
        }

        public async Task<DailyGoalDto> Handle(CreateDailyGoalCommand request, CancellationToken cancellationToken)
        {
            var dailyGoal = _dailyGoalFactory.CreateNewUsersDailyGoal(request.UserId);

            await _unitOfWork.DailyGoals.Add(dailyGoal);
            await _unitOfWork.Save();

            dailyGoal = await _unitOfWork.DailyGoals.GetById(request.UserId);

            return _dailyGoalFactory.CreateDailyGoalDto(dailyGoal);
        }
    }
}
