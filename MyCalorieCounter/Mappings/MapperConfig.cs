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
            CreateMap<AddMealVM, ProductDto>().ReverseMap();


            CreateMap<ExerciseDto, ExerciseVM>().ReverseMap();
            CreateMap<ExerciseDto, AddActivityVM>()
                .ForMember(dest => dest.ExerciseId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

            CreateMap<DailyGoalVM, DailyGoalDto>().ReverseMap();

            CreateMap<EditMealVM, MealDto>().ReverseMap();
            CreateMap<AddMealVM, MealDto>().ReverseMap();

            CreateMap<EditActivityVM, MyActivityDto>().ReverseMap();
            CreateMap<AddActivityVM, MyActivityDto>().ReverseMap();
        }
    }
}
