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
    class MealConfiguration : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.HasOne(m => m.Product)
                   .WithMany()
                   .HasForeignKey(m => m.ProductId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(m => m.Date)
                   .IsRequired();

            builder.Property(m => m.UserId)
                   .IsRequired();
        }
    }
}
