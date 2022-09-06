using FluentValidation;
using MyCalorieCounter.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Validators
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(100).WithMessage("{PropertyName} can't be longer than 100 characters");

            RuleFor(p => p.Calories)
                .GreaterThanOrEqualTo(0).WithMessage("The amount of {PropertyName} can't be less than 0")
                .LessThan(1000).WithMessage("The amount of {ProperyName} can't be greater than 1000");

            RuleFor(p => p.Proteins)
                .GreaterThanOrEqualTo(0).WithMessage("The amount of {PropertyName} can't be less than 0")
                .LessThan(100).WithMessage("The amount of {ProperyName} can't be greater than 100");

            RuleFor(p => p.Carbs)
                .GreaterThanOrEqualTo(0).WithMessage("The amount of {PropertyName} can't be less than 0")
                .LessThan(100).WithMessage("The amount of {ProperyName} can't be greater than 100");

            RuleFor(p => p.Fats)
                .GreaterThanOrEqualTo(0).WithMessage("The amount of {PropertyName} can't be less than 0")
                .LessThan(100).WithMessage("The amount of {ProperyName} can't be greater than 100");

        }
    }
}
