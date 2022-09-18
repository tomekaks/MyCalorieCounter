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
    public class MealDtoValidator : AbstractValidator<MealDto>, IMealDtoValidator
    {
        public MealDtoValidator()
        {
            RuleFor(m => m.Weight)
                .NotEmpty().WithMessage("{ProperyName} is required")
                .LessThan(10000).WithMessage("{PropetyName} can't be greater than 10000");
        }
    }
}
