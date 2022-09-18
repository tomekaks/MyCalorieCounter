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
    public class DailyGoalDtoValidator : AbstractValidator<DailyGoalDto>, IDailyGoalDtoValidator
    {
        public DailyGoalDtoValidator()
        {
            RuleFor(p => p.Calories)
                .GreaterThanOrEqualTo(0).WithMessage("The amount of {PropertyName} can't be less than 0")
                .LessThan(100000).WithMessage("The amount of {ProperyName} can't be greater than 100000");

            RuleFor(p => p.Proteins)
                .GreaterThanOrEqualTo(0).WithMessage("The amount of {PropertyName} can't be less than 0")
                .LessThan(10000).WithMessage("The amount of {ProperyName} can't be greater than 10000");

            RuleFor(p => p.Carbs)
                .GreaterThanOrEqualTo(0).WithMessage("The amount of {PropertyName} can't be less than 0")
                .LessThan(10000).WithMessage("The amount of {ProperyName} can't be greater than 10000");

            RuleFor(p => p.Fats)
                .GreaterThanOrEqualTo(0).WithMessage("The amount of {PropertyName} can't be less than 0")
                .LessThan(10000).WithMessage("The amount of {ProperyName} can't be greater than 10000");
        }
    }
}
