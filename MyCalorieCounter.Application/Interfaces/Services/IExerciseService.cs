using MyCalorieCounter.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Interfaces.Services
{
    public interface IExerciseService
    {
        Task<ExerciseDto> GetExercise(int id);
        Task AddNewExercise(ExerciseDto exerciseDto);
        Task AddNewExercise(string name, int calories);
        Task DeleteExercise(ExerciseDto exerciseDto);
        Task<List<ExerciseDto>> GetAllExercises();
    }
}
