using AutoMapper;
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
            CreateMap<AddActivityVM, ProductDto>().ReverseMap();
            CreateMap<AddFoodVM, ProductDto>().ReverseMap();

            CreateMap<ExerciseVM, ExerciseDto>().ReverseMap();

            CreateMap<DailyGoalVM, DailyGoalDto>().ReverseMap();

            CreateMap<RemoveMealVM, MealDto>().ReverseMap();
            CreateMap<EditMealVM, MealDto>().ReverseMap();
            CreateMap<AddFoodVM, MealDto>().ReverseMap();

            CreateMap<RemoveActivityVM, MyActivityDto>().ReverseMap();
            CreateMap<EditActivityVM, MyActivityDto>().ReverseMap();
            CreateMap<AddActivityVM, MyActivityDto>().ReverseMap();
        }
    }
}
