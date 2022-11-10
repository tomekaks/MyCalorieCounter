using MediatR;
using MyCalorieCounter.Application.CQRS.MyActivity.Requests.Queries;
using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.MyActivity.Handlers.Queries
{
    public class GetMyActivityRequestHandler : IRequestHandler<GetMyActivityRequest, MyActivityDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyActivityFactory _myActivityFactory;

        public GetMyActivityRequestHandler(IUnitOfWork unitOfWork, IMyActivityFactory myActivityFactory)
        {
            _unitOfWork = unitOfWork;
            _myActivityFactory = myActivityFactory;
        }

        public async Task<MyActivityDto> Handle(GetMyActivityRequest request, CancellationToken cancellationToken)
        {
            var myActivity = await _unitOfWork.MyActivities.Get(q => q.Id == request.Id, includeProperties:"Exercise");
            return _myActivityFactory.CreateMyActivityDto(myActivity);
        }
    }
}
