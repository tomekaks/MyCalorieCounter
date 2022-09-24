﻿using Microsoft.Extensions.DependencyInjection;
using MyCalorieCounter.Application.Factories;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Application.Interfaces.Repositories;
using MyCalorieCounter.Application.Interfaces.Services;
using MyCalorieCounter.Application.Interfaces.Validators;
using MyCalorieCounter.Application.Services;
using MyCalorieCounter.Application.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application
{
    public static class ApplicationServicesConfiguration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDailySumService, DailySumService>();
            services.AddScoped<IDailyGoalService, DailyGoalService>();
            services.AddScoped<IMealService, MealService>();
            services.AddScoped<IMyActivityService, MyActivityService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IExerciseService, ExerciseService>();
            services.AddScoped<ISettingService, SettingService>();

            services.AddScoped<IDailySumFactory, DailySumFactory>();
            services.AddScoped<IDailyGoalFactory, DailyGoalFactory>();
            services.AddScoped<IMealFactory, MealFactory>();
            services.AddScoped<IMyActivityFactory, MyActivityFactory>();
            services.AddScoped<IProductFactory, ProductFactory>();
            services.AddScoped<IExerciseFactory, ExerciseFactory>();
            
            services.AddScoped<IDailyGoalDtoValidator, DailyGoalDtoValidator>();
            services.AddScoped<IMealDtoValidator, MealDtoValidator>();
            services.AddScoped<IMyActivityDtoValidator, MyActivityDtoValidator>();
            services.AddScoped<IProductDtoValidator, ProductDtoValidator>();
            services.AddScoped<IExerciseDtoValidator, ExerciseDtoValidator>();

            return services;
        }
    }
}
