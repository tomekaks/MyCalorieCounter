using MyCalorieCounter.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Interfaces.Services
{
    public interface IMyActivityService
    {
        Task AddActivity(MyActivityDto myActivityDto);
        Task<List<MyActivityDto>> GetTodaysActivities(int dailySumId);
        Task<MyActivityDto> GetMyActivity(int id);
        Task DeleteActivity(int id);
        Task UpdateActivity(MyActivityDto myActivityDto);
    }
}
