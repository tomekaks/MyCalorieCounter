using MediatR;
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
    public class GetDailySumListRequestHandler : IRequestHandler<GetDailySumListRequest, List<DailySumDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDailySumFactory _dailySumFactory;

        public GetDailySumListRequestHandler(IUnitOfWork unitOfWork, IDailySumFactory dailySumFactory)
        {
            _unitOfWork = unitOfWork;
            _dailySumFactory = dailySumFactory;
        }

        public async Task<List<DailySumDto>> Handle(GetDailySumListRequest request, CancellationToken cancellationToken)
        {
            var dailySumList = await _unitOfWork.DailySums.GetAll(q => q.UserId == request.UserId);
            return _dailySumFactory.CreateDailySumDtoList(dailySumList.ToList());
        }
    }
}
