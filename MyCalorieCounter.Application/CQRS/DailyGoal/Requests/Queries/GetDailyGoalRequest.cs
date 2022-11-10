using MediatR;
using MyCalorieCounter.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.DailyGoal.Requests.Queries
{
    public class GetDailyGoalRequest : IRequest<DailyGoalDto>
    {
        public string UserId { get; set; }
    }
}
