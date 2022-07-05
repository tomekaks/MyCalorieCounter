using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Factories
{
    public class ExerciseFactory : IExerciseFactory
    {
        public Exercise CreateExercise(ExerciseDto exerciseDto)
        {
            return new Exercise()
            {
                Name = exerciseDto.Name,
                CaloriesPerHour = exerciseDto.CaloriesPerHour
            };
        }
        public ExerciseDto CreateExerciseDto(Exercise exercise)
        {
            return new ExerciseDto()
            {
                Id = exercise.Id,
                Name = exercise.Name,
                CaloriesPerHour = exercise.CaloriesPerHour
            };
        }
    }
}
