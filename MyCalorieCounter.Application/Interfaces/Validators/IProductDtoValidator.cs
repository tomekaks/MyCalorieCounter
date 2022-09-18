﻿using FluentValidation.Results;
using MyCalorieCounter.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Interfaces.Validators
{
    public interface IProductDtoValidator
    {
        ValidationResult Validate(ProductDto productDto);
    }
}
