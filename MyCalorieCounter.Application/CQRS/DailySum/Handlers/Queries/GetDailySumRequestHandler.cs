using MediatR;
using MyCalorieCounter.Application.CQRS.DailySum.Requests.Commands;
using MyCalorieCounter.Application.CQRS.DailySum.Requests.Queries;
using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.DailySum.Handlers.Queries
{
    public class GetDailySumRequestHandler : IRequestHandler<GetDailySumRequest, DailySumDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDailySumFactory _dailySumFactory;
        private readonly IMediator _mediator;

        public GetDailySumRequestHandler(IUnitOfWork unitOfWork, IDailySumFactory dailySumFactory, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _dailySumFactory = dailySumFactory;
            _mediator = mediator;
        }

        public async Task<DailySumDto> Handle(GetDailySumRequest request, CancellationToken cancellationToken)
        {
            var dailySum = await _unitOfWork.DailySums.Get(q => q.Date == request.Date && q.UserId == request.UserId);

            if (dailySum == null)
            {
                return await _mediator.Send(new CreateDailySumCommand { Date = request.Date, UserId = request.UserId });
            }

            return _dailySumFactory.CreateDailySumDto(dailySum);
        }
    }
}
