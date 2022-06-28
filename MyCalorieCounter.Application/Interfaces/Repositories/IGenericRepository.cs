using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class 
    {
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression = null, string includeProperties = null);
        Task<T> Get(Expression<Func<T, bool>> expression);
        Task<bool> IsExists(Expression<Func<T, bool>> expression);
        Task Add(T entity);
        void Delete(T entity);

    }
}
