using MediatR;
using MyCalorieCounter.Application.CQRS.DailySum.Requests.Commands;
using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.DailySum.Handlers.Commands
{
    public class CreateDailySumCommandHandler : IRequestHandler<CreateDailySumCommand, DailySumDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDailySumFactory _dailySumFactory;


        public CreateDailySumCommandHandler(IUnitOfWork unitOfWork, IDailySumFactory dailySumFactory)
        {
            _unitOfWork = unitOfWork;
            _dailySumFactory = dailySumFactory;
        }

        public async Task<DailySumDto> Handle(CreateDailySumCommand request, CancellationToken cancellationToken)
        {
            var dailySum = _dailySumFactory.CreateDailySum(request.Date, request.UserId);

            await _unitOfWork.DailySums.Add(dailySum);
            await _unitOfWork.Save();

            return _dailySumFactory.CreateDailySumDto(dailySum);
        }
    }
}
