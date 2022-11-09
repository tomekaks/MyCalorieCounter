using MediatR;
using MyCalorieCounter.Application.CQRS.Exercise.Requests.Commands;
using MyCalorieCounter.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.Exercise.Handlers.Commands
{
    public class DeleteExerciseCommandHandler : IRequestHandler<DeleteExerciseCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteExerciseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
        {
            var exercise = await _unitOfWork.Exercises.Get(q => q.Id == request.Id);

            _unitOfWork.Exercises.Delete(exercise);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
