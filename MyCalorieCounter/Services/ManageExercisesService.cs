using AutoMapper;
using MediatR;
using MyCalorieCounter.Application.CQRS.Exercise.Requests.Commands;
using MyCalorieCounter.Application.CQRS.Exercise.Requests.Queries;
using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Interefaces.Services;
using MyCalorieCounter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalorieCounter.Services
{
    public class ManageExercisesService : IManageExercisesService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ManageExercisesService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task AddExercise(ExerciseVM model)
        {
            var exercise = _mapper.Map<ExerciseDto>(model);
            
            await _mediator.Send(new CreateExerciseCommand { ExerciseDto = exercise });
        }

        public async Task DeleteExercise(int id)
        {
            await _mediator.Send(new DeleteExerciseCommand { Id = id });
        }

        public async Task EditExercise(ExerciseVM model)
        {
            var exercise = _mapper.Map<ExerciseDto>(model);

            await _mediator.Send(new UpdateExerciseCommand { ExerciaseDto = exercise });
        }

        public async Task<ExerciseVM> GetExercise(int id)
        {
            var exerciseDto = await _mediator.Send(new GetExerciseRequest { Id = id });

            return _mapper.Map<ExerciseVM>(exerciseDto);
        }

        public async Task<ActivitiesVM> GetExerciseList()
        {
            var exercises = await _mediator.Send(new GetExerciseListRequest());

            return new ActivitiesVM()
            {
                Exercises = exercises
            };
        }
    }
}
