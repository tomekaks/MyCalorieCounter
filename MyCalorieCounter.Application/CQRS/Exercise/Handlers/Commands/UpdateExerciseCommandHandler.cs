using MediatR;
using MyCalorieCounter.Application.CQRS.Exercise.Requests.Commands;
using MyCalorieCounter.Application.Exeptions;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Application.Interfaces.Repositories;
using MyCalorieCounter.Application.Interfaces.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.Exercise.Handlers.Commands
{
    public class UpdateExerciseCommandHandler : IRequestHandler<UpdateExerciseCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExerciseFactory _exerciseFactory;
        private readonly IExerciseDtoValidator _exerciseDtoValidator;

        public UpdateExerciseCommandHandler(IUnitOfWork unitOfWork, IExerciseFactory exerciseFactory, IExerciseDtoValidator exerciseDtoValidator)
        {
            _unitOfWork = unitOfWork;
            _exerciseFactory = exerciseFactory;
            _exerciseDtoValidator = exerciseDtoValidator;
        }

        public async Task<Unit> Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _exerciseDtoValidator.Validate(request.ExerciaseDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationExeption(validationResult);
            }

            var exercise = await _unitOfWork.Exercises.Get(q => q.Id == request.ExerciaseDto.Id);
            exercise = _exerciseFactory.MapToModel(exercise, request.ExerciaseDto);

            _unitOfWork.Exercises.Update(exercise);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
