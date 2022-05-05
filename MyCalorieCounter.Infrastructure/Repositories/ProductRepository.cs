using MyCalorieCounter.Application.Interfaces.Repositories;
using MyCalorieCounter.Core.Data;
using MyCalorieCounter.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Update(Product product)
        {
            var obj = await _context.Products.FirstOrDefaultAsync(p => p.Name == product.Name);
            if (obj != null)
            {
                obj.Calories = product.Calories;
                obj.Proteins = product.Proteins;
                obj.Carbs = product.Carbs;
                obj.Fats = product.Fats;
            }
        }
    }
}
