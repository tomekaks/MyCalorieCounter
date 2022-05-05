using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyCalorieCounter.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Infrastructure.DataBase
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<DailySum> DailySums { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired();

            builder.Entity<DailySum>()
                .Property(p => p.Date)
                .IsRequired();

            builder.Entity<Setting>()
                .Property(p => p.Key)
                .IsRequired();
            builder.Entity<Setting>()
               .Property(p => p.Value)
               .IsRequired();


            base.OnModelCreating(builder);
        }
    }
}
