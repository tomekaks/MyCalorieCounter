using MediatR;
using MyCalorieCounter.Application.CQRS.DailySum.Requests.Commands;
using MyCalorieCounter.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.DailySum.Handlers.Commands
{
    public class DeleteDailySumCommandHandler : IRequestHandler<DeleteDailySumCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDailySumCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteDailySumCommand request, CancellationToken cancellationToken)
        {
            var dailySum = await _unitOfWork.DailySums.Get(q => q.Id == request.Id);

            _unitOfWork.DailySums.Delete(dailySum);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
