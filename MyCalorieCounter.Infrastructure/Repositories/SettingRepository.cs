using Microsoft.EntityFrameworkCore;
using MyCalorieCounter.Application.Interfaces.Repositories;
using MyCalorieCounter.Core.Data;
using MyCalorieCounter.Infrastructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Infrastructure.Repositories
{
    public class SettingRepository : GenericRepository<Setting>, ISettingRepository
    {
        private readonly ApplicationDbContext _context;
        public SettingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Update(Setting setting)
        {
            var obj = await _context.Settings.FirstOrDefaultAsync(s => s.Key == setting.Key);
            if (obj != null)
            {
                obj.Value = setting.Value;
            }
        }
    }
}
