using Microsoft.EntityFrameworkCore;
using MyCalorieCounter.Application.Interfaces.Repositories;
using MyCalorieCounter.Core.Data;
using MyCalorieCounter.Infrastructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Infrastructure.Repositories
{
    public class MealRepository : GenericRepository<Meal>, IMealRepository
    {
        private readonly ApplicationDbContext _context;
        public MealRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task Update(Meal meal)
        {
            var obj = await _context.Meals.FirstOrDefaultAsync(m => m.Id == meal.Id && m.UserId == meal.UserId);
            if (obj != null)
            {
                obj.Weight = meal.Weight;
            }
            
        }
    }
}
