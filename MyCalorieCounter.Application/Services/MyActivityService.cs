﻿using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Application.Interfaces.Repositories;
using MyCalorieCounter.Application.Interfaces.Services;
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

        public MyActivityService(IMyActivityFactory myActivityFactory, IUnitOfWork unitOfWork)
        {
            _myActivityFactory = myActivityFactory;
            _unitOfWork = unitOfWork;
        }

        public async Task AddActivity(string userId, int exerciseId, int minutes, int calories, int dailySumId)
        {
            var myActivityDto = _myActivityFactory.CreateMyActivityDto(userId, exerciseId, minutes, calories, dailySumId);
            var activity = _myActivityFactory.CreateMyActivity(myActivityDto);
            await _unitOfWork.MyActivities.Add(activity);
            await _unitOfWork.Save();
        }
    }
}
