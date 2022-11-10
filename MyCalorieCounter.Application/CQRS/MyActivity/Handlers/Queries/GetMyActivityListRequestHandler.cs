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
    public class GetMyActivityListRequestHandler : IRequestHandler<GetMyActivityListRequest, List<MyActivityDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyActivityFactory _myActivityFactory;

        public GetMyActivityListRequestHandler(IUnitOfWork unitOfWork, IMyActivityFactory myActivityFactory)
        {
            _unitOfWork = unitOfWork;
            _myActivityFactory = myActivityFactory;
        }

        public async Task<List<MyActivityDto>> Handle(GetMyActivityListRequest request, CancellationToken cancellationToken)
        {
            var myActivityList = await _unitOfWork.MyActivities.GetAll(q => q.DailySumId == request.DailySumId, includeProperties:"Exercise");
            return _myActivityFactory.CreateMyActivityDtoList(myActivityList.ToList());
        }
    }
}
