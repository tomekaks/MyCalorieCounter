using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Core.Data
{
    public class MyActivity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int DailySumId { get; set; }
        public int Calories { get; set; }
        public int Minutes { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
    }
}
