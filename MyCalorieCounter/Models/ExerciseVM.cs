using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalorieCounter.Models
{
    public class ExerciseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Range(1, 1000)]
        [DisplayName("Calories Per Hour")]
        public int CaloriesPerHour { get; set; }
    }
}
