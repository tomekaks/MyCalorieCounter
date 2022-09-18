using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Application.Interfaces.Repositories;
using MyCalorieCounter.Application.Interfaces.Services;
using MyCalorieCounter.Application.Interfaces.Validators;
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
        private readonly IExerciseDtoValidator _exerciseDtoValidator;

        public ExerciseService(IExerciseFactory exerciseFactory, IUnitOfWork unitOfWork, IExerciseDtoValidator exerciseDtoValidator)
        {
            _exerciseFactory = exerciseFactory;
            _unitOfWork = unitOfWork;
            _exerciseDtoValidator = exerciseDtoValidator;
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

            var validationResult = _exerciseDtoValidator.Validate(exerciseDto);
            if (!validationResult.IsValid)
            {
                throw new Exception();
            }

            var exercise = _exerciseFactory.CreateExercise(exerciseDto);
            await _unitOfWork.Exercises.Add(exercise);
            await _unitOfWork.Save();
        }

        public async Task DeleteExercise(int id)
        {
            var exercise = await _unitOfWork.Exercises.Get(e => e.Id == id);
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

        public async Task UpdateExercise(ExerciseDto exerciseDto, int id)
        {
            var validationResult = _exerciseDtoValidator.Validate(exerciseDto);
            if (!validationResult.IsValid)
            {
                throw new Exception();
            }

            var exercise = _exerciseFactory.CreateExercise(exerciseDto, id);
            await _unitOfWork.Exercises.Update(exercise);
            await _unitOfWork.Save();
        }
    }
}
