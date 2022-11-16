using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Exeptions;
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
    public class MyActivityService : IMyActivityService
    {
        private readonly IMyActivityFactory _myActivityFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyActivityDtoValidator _myActivityDtoValidator;

        public MyActivityService(IMyActivityFactory myActivityFactory, IUnitOfWork unitOfWork, IMyActivityDtoValidator myActivityDtoValidator)
        {
            _myActivityFactory = myActivityFactory;
            _unitOfWork = unitOfWork;
            _myActivityDtoValidator = myActivityDtoValidator;
        }

        public async Task AddActivity(MyActivityDto myActivityDto)
        {
            var validationResult = _myActivityDtoValidator.Validate(myActivityDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationExeption(validationResult);
            }

            var activity = _myActivityFactory.CreateMyActivity(myActivityDto);
            await _unitOfWork.MyActivities.Add(activity);
            await _unitOfWork.Save();
        }
        public async Task<List<MyActivityDto>> GetTodaysActivities(int dailySumId)
        {
            var activityList = await _unitOfWork.MyActivities.GetAll(a => a.DailySumId == dailySumId, includeProperties:"Exercise");
            return _myActivityFactory.CreateMyActivityDtoList(activityList.ToList());
        }
        public async Task<MyActivityDto> GetMyActivity(int id)
        {
            var activity = await _unitOfWork.MyActivities.Get(a => a.Id == id, includeProperties: "Exercise");
            return _myActivityFactory.CreateMyActivityDto(activity);
        }
        public async Task DeleteActivity(int id)
        {
            var activity = await _unitOfWork.MyActivities.Get(a => a.Id == id);
            _unitOfWork.MyActivities.Delete(activity);
            await _unitOfWork.Save();
        }
        public async Task UpdateActivity(MyActivityDto myActivityDto)
        {
            var validationResult = _myActivityDtoValidator.Validate(myActivityDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationExeption(validationResult);
            }

            var myActivity = await _unitOfWork.MyActivities.Get(q => q.Id == myActivityDto.Id);
            myActivity = _myActivityFactory.MapToModel(myActivity, myActivityDto);

            _unitOfWork.MyActivities.Update(myActivity);
            await _unitOfWork.Save();
        }
    }
}
