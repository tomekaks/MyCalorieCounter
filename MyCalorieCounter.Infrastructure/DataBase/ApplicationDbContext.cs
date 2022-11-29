using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyCalorieCounter.Core.Data;
using MyCalorieCounter.Infrastructure.DataBase.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Infrastructure.DataBase
{
    public class ApplicationDbContext : IdentityDbContext<Core.Data.ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<DailySum> DailySums { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<DailyGoal> DailyGoals { get; set; }
        public DbSet<MyActivity> MyActivities { get; set; }
        public DbSet<Exercise> Exercises { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new DailySumConfiguration());
            builder.ApplyConfiguration(new MealConfiguration());
            builder.ApplyConfiguration(new MyActivityConfiguration());

            builder.Entity<ApplicationUser>()
                .HasOne(a => a.DailyGoal)
                .WithOne(d => d.User);
            
            // Obsolete after refactoring, might be needed later
            //builder.Entity<Setting>()
            //.Property(s => s.Key)
            //.IsRequired();
            //builder.Entity<Setting>()
            //   .Property(s => s.Value)
            //   .IsRequired();

            base.OnModelCreating(builder);
        }
    }
}
