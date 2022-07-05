using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Dto
{
    public class MyActivityDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int DailySumId { get; set; }
        public int Calories { get; set; }
        public int Minutes { get; set; }
        public int ExerciseId { get; set; }
        public ExerciseDto Exercise { get; set; }
    }
}
