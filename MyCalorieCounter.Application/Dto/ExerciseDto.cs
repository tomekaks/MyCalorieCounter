using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Dto
{
    public class ExerciseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CaloriesPerHour { get; set; }
    }
}
