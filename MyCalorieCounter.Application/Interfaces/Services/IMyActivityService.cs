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
        Task AddActivity(string userId, int exerciseId, int minutes, int calories, int dailySumId);
        Task<List<MyActivityDto>> GetTodaysActivities(int dailySumId);
    }
}
