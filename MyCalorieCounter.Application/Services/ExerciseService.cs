using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Application.Interfaces.Repositories;
using MyCalorieCounter.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Services
{
    public class ExerciseService : IExerciseService
    { 
        private readonly IExerciseFactory _exerciseFactory;
        private readonly IUnitOfWork _unitOfWork;

        public ExerciseService(IExerciseFactory exerciseFactory, IUnitOfWork unitOfWork)
        {
            _exerciseFactory = exerciseFactory;
            _unitOfWork = unitOfWork;
        }

        public async Task AddNewExercise(ExerciseDto exerciseDto)
        {
            var exercise = _exerciseFactory.CreateExercise(exerciseDto);
            await _unitOfWork.Exercises.Add(exercise);
            await _unitOfWork.Save();
        }

        public async Task AddNewExercise(string name, int calories)
        {
            var exerciseDto = _exerciseFactory.CreateExerciseDto(name, calories);
            var exercise = _exerciseFactory.CreateExercise(exerciseDto);
            await _unitOfWork.Exercises.Add(exercise);
            await _unitOfWork.Save();
        }

        public async Task DeleteExercise(ExerciseDto exerciseDto)
        {
            var exercise = _exerciseFactory.CreateExercise(exerciseDto);
            _unitOfWork.Exercises.Delete(exercise);
            await _unitOfWork.Save();
        }

        public async Task<List<ExerciseDto>> GetAllExercises()
        {
            var exercises = await _unitOfWork.Exercises.GetAll();
            if (exercises == null)
            {
                return new List<ExerciseDto>();
            }
            return _exerciseFactory.CreateExerciseDtoList(exercises.ToList());
        }

        public async Task<ExerciseDto> GetExercise(int id)
        {
            var exercise = await _unitOfWork.Exercises.Get(e => e.Id == id);
            return _exerciseFactory.CreateExerciseDto(exercise);
        }
    }
}
