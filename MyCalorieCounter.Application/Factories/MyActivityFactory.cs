﻿using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Factories
{
    public class MyActivityFactory : IMyActivityFactory
    {
        private readonly IExerciseFactory _exerciseFactory;

        public MyActivityFactory(IExerciseFactory exerciseFactory)
        {
            _exerciseFactory = exerciseFactory;
        }

        public MyActivity CreateMyActivity(MyActivityDto myActivityDto)
        {
            return new MyActivity()
            {
                UserId = myActivityDto.UserId,
                Calories = myActivityDto.Calories,
                DailySumId = myActivityDto.DailySumId,
                ExerciseId = myActivityDto.ExerciseId,
                Minutes = myActivityDto.Minutes
            };
        }

        public MyActivityDto CreateMyActivityDto(MyActivity myActivity)
        {
            return new MyActivityDto()
            {
                Id = myActivity.Id,
                UserId = myActivity.UserId,
                Calories = myActivity.Calories,
                DailySumId = myActivity.DailySumId,
                ExerciseId = myActivity.ExerciseId,
                Exercise = _exerciseFactory.CreateExerciseDto(myActivity.Exercise),
                Minutes = myActivity.Minutes
            };
        }
    }
}
