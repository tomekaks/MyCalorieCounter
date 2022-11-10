using MediatR;
using MyCalorieCounter.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.DailyGoal.Handlers.Queries
{
    public class UpdateDailyGoalCommand : IRequest
    {
        public DailyGoalDto DailyGoalDto { get; set; }
    }
}
