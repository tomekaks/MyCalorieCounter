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
    public class GetExerciseListRequestHandler : IRequestHandler<GetExerciseListRequest, List<ExerciseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExerciseFactory _exerciseFactory;

        public GetExerciseListRequestHandler(IUnitOfWork unitOfWork, IExerciseFactory exerciseFactory)
        {
            _unitOfWork = unitOfWork;
            _exerciseFactory = exerciseFactory;
        }

        public async Task<List<ExerciseDto>> Handle(GetExerciseListRequest request, CancellationToken cancellationToken)
        {
            var exerciseList = await _unitOfWork.Exercises.GetAll();
            return _exerciseFactory.CreateExerciseDtoList(exerciseList.ToList());
        }
    }
}
