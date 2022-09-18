using System;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCalorieCounter.Application.Dto;

namespace MyCalorieCounter.Application.Interfaces.Validators
{
    public interface IDailyGoalDtoValidator
    {
        ValidationResult Validate(DailyGoalDto dailyGoalDto);
    }
}
