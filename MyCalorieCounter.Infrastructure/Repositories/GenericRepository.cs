using Microsoft.EntityFrameworkCore;
using MyCalorieCounter.Application.Interfaces.Repositories;
using MyCalorieCounter.Infrastructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _db;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }

        public async Task Add(T entity)
        {
            await _db.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _db.Remove(entity);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> query = _db;
            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression = null)
        {
            IQueryable<T> query = _db;

            if (expression != null)
            {
                query = query.Where(expression);
            }
            return await query.ToListAsync();
        }

        public async Task<bool> IsExists(Expression<Func<T, bool>> expression = null)
        {
            IQueryable<T> query = _db;
            return await query.AnyAsync(expression);
        }
    }
}
