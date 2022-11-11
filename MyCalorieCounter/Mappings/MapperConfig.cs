﻿using AutoMapper;
using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalorieCounter.Mappings
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<ProductVM, ProductDto>().ReverseMap();

            CreateMap<ExerciseVM, ExerciseDto>().ReverseMap();
        }
    }
}
