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
    public class MealService : IMealService
    {
        private readonly IMealFactory _mealFactory;
        private readonly IUnitOfWork _unitOfWork;

        public MealService(IMealFactory mealFactory, IUnitOfWork unitOfWork)
        {
            _mealFactory = mealFactory;
            _unitOfWork = unitOfWork;
        }
    }
}
