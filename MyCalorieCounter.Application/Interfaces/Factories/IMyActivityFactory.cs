using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Interfaces.Factories
{
    public interface IMyActivityFactory
    {
        MyActivity CreateMyActivity(MyActivityDto myActivityDto);
        MyActivity CreateMyActivity(MyActivityDto myActivityDto, int id);
        MyActivityDto CreateMyActivityDto(MyActivity myActivity);
        MyActivityDto CreateMyActivityDto(string userId, int exerciseId, int minutes, int calories, int dailySumId);
        List<MyActivityDto> CreateMyActivityDtoList(List<MyActivity> myActivities);
        MyActivity MapToModel(MyActivity model, MyActivityDto dto);
    }
}
