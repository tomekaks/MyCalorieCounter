using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCalorieCounter.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Infrastructure.DataBase.Configurations
{
    class DailySumConfiguration : IEntityTypeConfiguration<DailySum>
    {
        public void Configure(EntityTypeBuilder<DailySum> builder)
        {
            builder.Property(d => d.Date)
                   .IsRequired();

            builder.HasMany(d => d.Meals)
                   .WithOne(m => m.DailySum);
                
            builder.HasOne(d => d.User)
                   .WithMany(u => u.DailySums)
                   .HasForeignKey(d => d.UserId);

        }
    }
}
