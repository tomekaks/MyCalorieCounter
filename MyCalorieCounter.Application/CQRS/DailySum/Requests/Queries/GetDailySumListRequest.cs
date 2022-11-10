using MediatR;
using MyCalorieCounter.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.DailySum.Requests.Queries
{
    public class GetDailySumListRequest : IRequest<List<DailySumDto>>
    {
        public string UserId { get; set; }
    }
}
