using MediatR;
using MyCalorieCounter.Application.CQRS.Exercise.Requests.Queries;
using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.Exercise.Handlers.Queries
{
    public class GetExerciseRequestHandler : IRequestHandler<GetExerciseRequest, ExerciseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExerciseFactory _exerciseFactory;

        public GetExerciseRequestHandler(IUnitOfWork unitOfWork, IExerciseFactory exerciseFactory)
        {
            _unitOfWork = unitOfWork;
            _exerciseFactory = exerciseFactory;
        }

        public async Task<ExerciseDto> Handle(GetExerciseRequest request, CancellationToken cancellationToken)
        {
            var exercise = await _unitOfWork.Exercises.Get(q => q.Id == request.Id);
            return _exerciseFactory.CreateExerciseDto(exercise);
        }
    }
}
