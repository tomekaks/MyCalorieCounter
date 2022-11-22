using MyCalorieCounter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalorieCounter.Interefaces.Services
{
    public interface IManageExercisesService
    {
        Task<ActivitiesVM> GetExerciseList();
        Task<ExerciseVM> GetExercise(int id);
        Task AddExercise(ExerciseVM model);
        Task DeleteExercise(int id);
        Task EditExercise(ExerciseVM model);
    }
}
