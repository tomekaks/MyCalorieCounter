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
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired();

            builder.Entity<DailySum>()
                .Property(d => d.Date)
                .IsRequired();
            builder.Entity<DailySum>()
                .HasMany(d => d.Meals)
                .WithOne(m => m.DailySum);
            builder.Entity<DailySum>()
                .HasOne(d => d.User)
                .WithMany(u => u.DailySums)
                .HasForeignKey(d => d.UserId);

            builder.Entity<Setting>()
                .Property(s => s.Key)
                .IsRequired();
            builder.Entity<Setting>()
               .Property(s => s.Value)
               .IsRequired();

            builder.Entity<Meal>()
                .HasOne(m => m.Product)
                .WithMany()
                .HasForeignKey(m => m.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ApplicationUser>()
                .HasOne(a => a.DailyGoal)
                .WithOne(d => d.User);
                
                
                       

            base.OnModelCreating(builder);
        }
    }
}
