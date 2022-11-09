using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.Exercise.Requests.Commands
{
    public class DeleteExerciseCommand : IRequest
    {
        public int Id { get; set; }
    }
}
