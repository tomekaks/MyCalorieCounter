using AutoMapper;
using MediatR;
using MyCalorieCounter.Application.CQRS.DailyGoal.Requests.Commands;
using MyCalorieCounter.Application.CQRS.DailyGoal.Requests.Queries;
using MyCalorieCounter.Application.CQRS.DailySum.Requests.Queries;
using MyCalorieCounter.Application.CQRS.Meal.Requests.Commands;
using MyCalorieCounter.Application.CQRS.Meal.Requests.Queries;
using MyCalorieCounter.Application.CQRS.MyActivity.Requests.Commands;
using MyCalorieCounter.Application.CQRS.MyActivity.Requests.Queries;
using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Interefaces.Services;
using MyCalorieCounter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalorieCounter.Services
{
    public class DailyOverviewService : IDailyOverviewService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DailyOverviewService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task DeleteActivity(int id)
        {
            await _mediator.Send(new DeleteMyActivityCommand { Id = id });
        }

        public async Task DeleteMeal(int id)
        {
            await _mediator.Send(new DeleteMealCommand { Id = id });
        }

        public async Task EditActivity(EditActivityVM model)
        {
            var activityDto = _mapper.Map<MyActivityDto>(model);
            await _mediator.Send(new UpdateMyActivityCommand { MyActivityDto = activityDto });
        }

        public async Task EditMeal(EditMealVM model)
        {
            var mealDto = _mapper.Map<MealDto>(model);
            await _mediator.Send(new UpdateMealCommand { MealDto = mealDto });
        }

        public async Task<DeleteActivityVM> GenerateDeleteActivityVM(int id)
        {
            var activity = await _mediator.Send(new GetMyActivityRequest { Id = id });
            return _mapper.Map<DeleteActivityVM>(activity);
        }

        public async Task<DeleteMealVM> GenerateDeleteMealVM(int id)
        {
            var meal = await _mediator.Send(new GetMealRequest { Id = id });
            return _mapper.Map<DeleteMealVM>(meal);
        }

        public async Task<EditActivityVM> GetActivity(int id)
        {
            var activity = await _mediator.Send(new GetMyActivityRequest { Id = id });
            return _mapper.Map<EditActivityVM>(activity);
        }

        public async Task<DailyGoalVM> GenerateDailyGoalVM(string userId)
        {
            var dailyGoalDto = await GetDailyGoal(userId);
            return _mapper.Map<DailyGoalVM>(dailyGoalDto);
        }

        public async Task<DailyOverviewVM> GetDailyOverview(string userId)
        {
            var dailySumDto = await GetDailySum(userId);
            var dailyGoalDto = await GetDailyGoal(userId);
            var mealDtoList = await GetMealList(dailySumDto.Id);
            var myActivityDtoList = await GetMyActivityList(dailySumDto.Id);
            return new DailyOverviewVM(dailySumDto, dailyGoalDto, mealDtoList, myActivityDtoList);
        }

        public async Task<EditMealVM> GetMeal(int id)
        {
            var meal = await _mediator.Send(new GetMealRequest { Id = id });
            return _mapper.Map<EditMealVM>(meal);
        }

        public async Task UpdateDailyGoal(DailyGoalVM model)
        {
            var dailyGoalDto = _mapper.Map<DailyGoalDto>(model);
            await _mediator.Send(new UpdateDailyGoalCommand { DailyGoalDto = dailyGoalDto });
        }

        private async Task<DailySumDto> GetDailySum(string userId)
        {
            var todaysDate = GetTodaysDate();
            return await _mediator.Send(new GetDailySumRequest { Date = todaysDate, UserId = userId });
        }
        private async Task<DailyGoalDto> GetDailyGoal(string userId)
        {
            return await _mediator.Send(new GetDailyGoalRequest { UserId = userId });
        }
        private async Task<List<MealDto>> GetMealList(int dailySumId)
        {
            return await _mediator.Send(new GetMealListRequest { DailySumId = dailySumId });
        }
        private async Task<List<MyActivityDto>> GetMyActivityList(int dailySumId)
        {
            return await _mediator.Send(new GetMyActivityListRequest { DailySumId = dailySumId });
        }
        private string GetTodaysDate()
        {
            return DateTime.Today.ToString("d");
        }
    }
}
