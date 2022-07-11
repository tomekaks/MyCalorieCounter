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
    public class ExerciseRepository : GenericRepository<Exercise>, IExerciseRepository
    {
        private readonly ApplicationDbContext _context;
        public ExerciseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Update(Exercise exercise)
        {
            var obj = await _context.Exercises.FirstOrDefaultAsync(q => q.Id == exercise.Id);
            if (obj != null)
            {
                obj.Name = exercise.Name;
                obj.CaloriesPerHour = exercise.CaloriesPerHour;
            }
        }
    }
}
