using MediatR;
using MyCalorieCounter.Application.CQRS.MyActivity.Requests.Commands;
using MyCalorieCounter.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.MyActivity.Handlers.Commands
{
    public class DeleteMyActivityCommandHandler : IRequestHandler<DeleteMyActivityCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMyActivityCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteMyActivityCommand request, CancellationToken cancellationToken)
        {
            var myActivity = await _unitOfWork.MyActivities.Get(q => q.Id == request.Id);
            _unitOfWork.MyActivities.Delete(myActivity);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
