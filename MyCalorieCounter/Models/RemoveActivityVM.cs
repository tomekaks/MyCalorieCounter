using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalorieCounter.Models
{
    public class RemoveActivityVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Minutes { get; set; }
        [DisplayName("Calories burned")]
        public int CaloriesBurned { get; set; }
    }
}
