using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Interfaces.Factories
{
    public interface IExerciseFactory
    {
        Exercise CreateExercise(ExerciseDto exerciseDto);
        ExerciseDto CreateExerciseDto(Exercise exercise);
        ExerciseDto CreateExerciseDto(string name, int calories);
        List<ExerciseDto> CreateExerciseDtoList(List<Exercise> exercises);
    }
}
