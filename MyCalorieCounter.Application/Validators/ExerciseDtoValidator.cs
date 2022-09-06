using FluentValidation;
using MyCalorieCounter.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Validators
{
    public class ExerciseDtoValidator : AbstractValidator<ExerciseDto>
    {
        public ExerciseDtoValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(100).WithMessage("{PropertyName} can't be longer than 100 characters");

            RuleFor(e => e.CaloriesPerHour)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} can't be less than 0")
                .LessThan(5000).WithMessage("{ProperyName} can't be greater than 5000");
        }
    }
}
