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
                CaloriesBurned = myActivityDto.CaloriesBurned,
                DailySumId = myActivityDto.DailySumId,
                ExerciseId = myActivityDto.ExerciseId,
                Minutes = myActivityDto.Minutes
            };
        }
        public MyActivity CreateMyActivity(MyActivityDto myActivityDto, int id)
        {
            return new MyActivity()
            {
                Id = id,
                UserId = myActivityDto.UserId,
                CaloriesBurned = myActivityDto.CaloriesBurned,
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
                CaloriesBurned = myActivity.CaloriesBurned,
                DailySumId = myActivity.DailySumId,
                ExerciseId = myActivity.ExerciseId,
                Exercise = _exerciseFactory.CreateExerciseDto(myActivity.Exercise),
                Minutes = myActivity.Minutes
            };
        }
        public MyActivityDto CreateMyActivityDto(string userId, int exerciseId, int minutes, int calories, int dailySumId)
        {
            return new MyActivityDto()
            {
                UserId = userId,
                CaloriesBurned = calories,
                DailySumId = dailySumId,
                ExerciseId = exerciseId,
                Minutes = minutes
            };
        }

        public List<MyActivityDto> CreateMyActivityDtoList(List<MyActivity> myActivities)
        {
            var activityList = new List<MyActivityDto>();
            foreach (var item in myActivities)
            {
                activityList.Add(CreateMyActivityDto(item));
            }
            return activityList;
        }

        public MyActivity MapToModel(MyActivity model, MyActivityDto dto)
        {
            model.Minutes = dto.Minutes;
            model.CaloriesBurned = dto.CaloriesBurned;

            return model;
        }
    }
}
