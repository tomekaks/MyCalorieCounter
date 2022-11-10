using MediatR;
using MyCalorieCounter.Application.CQRS.DailyGoal.Requests.Queries;
using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.DailyGoal.Handlers.Queries
{
    public class GetDailyGoalRequestHandler : IRequestHandler<GetDailyGoalRequest, DailyGoalDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDailyGoalFactory _dailyGoalFactory;
        public GetDailyGoalRequestHandler(IUnitOfWork unitOfWork, IDailyGoalFactory dailyGoalFactory)
        {
            _unitOfWork = unitOfWork;
            _dailyGoalFactory = dailyGoalFactory;
        }

        public async Task<DailyGoalDto> Handle(GetDailyGoalRequest request, CancellationToken cancellationToken)
        {
            var dailyGoal = await _unitOfWork.DailyGoals.GetById(request.UserId);
            return _dailyGoalFactory.CreateDailyGoalDto(dailyGoal);
        }
    }
}
