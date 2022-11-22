using AutoMapper;
using MediatR;
using MyCalorieCounter.Application.CQRS.Exercise.Requests.Queries;
using MyCalorieCounter.Application.CQRS.MyActivity.Requests.Commands;
using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Interefaces.Services;
using MyCalorieCounter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalorieCounter.Services
{
    public class ActivitiesService : IActivitiesService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ActivitiesService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task AddActivity(AddActivityVM model, string userId)
        {
            var myActivity = _mapper.Map<MyActivityDto>(model);
            myActivity.UserId = userId;

            await _mediator.Send(new CreateMyActivityCommand { MyActivityDto = myActivity });
        }

        public async Task<AddActivityVM> GetActivity(int id)
        {
            var exerciseDto = await _mediator.Send(new GetExerciseRequest { Id = id });

            return _mapper.Map<AddActivityVM>(exerciseDto);
        }

        public async Task<ActivitiesVM> GetActivityList()
        {
            var exercises = await _mediator.Send(new GetExerciseListRequest());

            return new ActivitiesVM
            {
                Exercises = exercises
            };
        }
    }
}
