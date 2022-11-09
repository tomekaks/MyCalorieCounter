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
    public class CreateExerciseCommandHandler : IRequestHandler<CreateExerciseCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExerciseFactory _exerciseFactory;
        private readonly IExerciseDtoValidator _exerciseDtoValidator;
        public CreateExerciseCommandHandler(IUnitOfWork unitOfWork, IExerciseFactory exerciseFactory, IExerciseDtoValidator exerciseDtoValidator)
        {
            _unitOfWork = unitOfWork;
            _exerciseFactory = exerciseFactory;
            _exerciseDtoValidator = exerciseDtoValidator;
        }

        public async Task<Unit> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
        {
            var validationRestult = _exerciseDtoValidator.Validate(request.ExerciseDto);
            if (!validationRestult.IsValid)
            {
                throw new ValidationExeption(validationRestult);
            }

            var exercise = _exerciseFactory.CreateExercise(request.ExerciseDto);
            await _unitOfWork.Exercises.Add(exercise);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
