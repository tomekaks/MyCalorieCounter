using MediatR;
using MyCalorieCounter.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.CQRS.Exercise.Requests.Commands
{
    public class UpdateExerciseCommand : IRequest
    {
        public ExerciseDto ExerciaseDto { get; set; }
    }
}
