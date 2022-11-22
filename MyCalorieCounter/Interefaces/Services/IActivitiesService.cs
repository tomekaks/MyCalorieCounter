using MyCalorieCounter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalorieCounter.Interefaces.Services
{
    public interface IActivitiesService
    {
        Task<ActivitiesVM> GetActivityList();
        Task<AddActivityVM> GetActivity(int id);
        Task AddActivity(AddActivityVM model, string userId);
    }
}
