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
    public class MyActivityRepository : GenericRepository<MyActivity>, IMyActivityRepository
    {
        private readonly ApplicationDbContext _context; 
        public MyActivityRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Update(MyActivity myActivity)
        {
            var obj = await _context.MyActivities.FirstOrDefaultAsync(q => q.Id == myActivity.Id);
            if (obj != null)
            {
                obj.Minutes = myActivity.Minutes;
                obj.Calories = myActivity.Calories;
            }
        }
    }
}
