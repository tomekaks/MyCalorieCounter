using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.DailySum.Requests.Commands
{
    public class DeleteDailySumCommand : IRequest
    {
        public int Id{ get; set; }
    }
}
