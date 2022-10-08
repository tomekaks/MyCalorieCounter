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
    class MyActivityConfiguration : IEntityTypeConfiguration<MyActivity>
    {
        public void Configure(EntityTypeBuilder<MyActivity> builder)
        {
            builder.HasOne(a => a.Exercise)
                   .WithMany()
                   .HasForeignKey(m => m.ExerciseId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
