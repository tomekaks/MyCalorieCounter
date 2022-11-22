using AutoMapper;
using MediatR;
using MyCalorieCounter.Application.CQRS.Meal.Requests.Commands;
using MyCalorieCounter.Application.CQRS.Product.Requsts.Queries;
using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Interefaces.Services;
using MyCalorieCounter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyCalorieCounter.Services
{
    public class AddMealsService : IAddMealsService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AddMealsService(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task AddMeal(AddMealVM model, string userId)
        {
            var meal = _mapper.Map<MealDto>(model);
            meal.UserId = userId;

            await _mediator.Send(new CreateMealCommand { MealDto = meal });
        }

        public async Task<AddMealVM> GetMeal(int id)
        {
            var productDto = await _mediator.Send(new GetProductRequest { Id = id });

            var model = _mapper.Map<AddMealVM>(productDto);
            model.ProductId = id;

            return model;
        }

        public async Task<AddMealListVM> GetMealList()
        {
            var productList = await _mediator.Send(new GetProductListRequest());

            return new AddMealListVM
            {
                Products = productList
            };
        }
 
    }
}
