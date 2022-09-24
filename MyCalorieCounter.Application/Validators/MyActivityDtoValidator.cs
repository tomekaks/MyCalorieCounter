using FluentValidation;
using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Interfaces.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Validators
{
    public class MyActivityDtoValidator : AbstractValidator<MyActivityDto>, IMyActivityDtoValidator
    {
        public MyActivityDtoValidator()
        {
            RuleFor(m => m.Minutes)
                .NotEmpty().WithMessage("{PropertyName} are required")
                .LessThan(1000).WithMessage("{ProperyName} have to be less than 1000");
        }
    }
}
