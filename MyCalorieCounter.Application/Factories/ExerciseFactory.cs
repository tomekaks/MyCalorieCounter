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
        public Exercise CreateExercise(ExerciseDto exerciseDto, int id)
        {
            return new Exercise()
            {
                Id = id,
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

        public ExerciseDto CreateExerciseDto(string name, int calories)
        {
            return new ExerciseDto()
            {
                Name = name,
                CaloriesPerHour = calories
            };
        }

        public List<ExerciseDto> CreateExerciseDtoList(List<Exercise> exercises)
        {
            var exerciseList = new List<ExerciseDto>();
            foreach (var item in exercises)
            {
                exerciseList.Add(CreateExerciseDto(item));
            }
            return exerciseList;
        }
        public Exercise MapToModel(Exercise model, ExerciseDto dto)
        {
            model.Name = dto.Name;
            model.CaloriesPerHour = dto.CaloriesPerHour;

            return model;
        }
    }
}
