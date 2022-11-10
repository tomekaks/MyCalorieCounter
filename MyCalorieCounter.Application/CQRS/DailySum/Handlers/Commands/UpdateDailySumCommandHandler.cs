using MediatR;
using MyCalorieCounter.Application.CQRS.DailySum.Requests.Commands;
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
    public class UpdateDailySumCommandHandler : IRequestHandler<UpdateDailySumCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDailySumFactory _dailySumFactory;

        public UpdateDailySumCommandHandler(IUnitOfWork unitOfWork, IDailySumFactory dailySumFactory)
        {
            _unitOfWork = unitOfWork;
            _dailySumFactory = dailySumFactory;
        }

        public async Task<Unit> Handle(UpdateDailySumCommand request, CancellationToken cancellationToken)
        {
            var dailySum = await _unitOfWork.DailySums.Get(q => q.Id == request.DailySumDto.Id);
            dailySum = _dailySumFactory.MapToModel(dailySum, request.DailySumDto);

            _unitOfWork.DailySums.Update(dailySum);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
