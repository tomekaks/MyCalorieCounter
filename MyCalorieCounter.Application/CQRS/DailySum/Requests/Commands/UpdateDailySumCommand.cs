using MediatR;
using MyCalorieCounter.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.DailySum.Requests.Commands
{
    public class UpdateDailySumCommand : IRequest
    {
        public DailySumDto DailySumDto { get; set; }
    }
}
