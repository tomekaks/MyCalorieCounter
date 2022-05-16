using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalorieCounter.Models
{
    public class AddFoodVM
    {
        public string Name { get; set; }
        public double Calories { get; set; }
        public double Proteins { get; set; }
        public double Carbs { get; set; }
        public double Fats { get; set; }
        public int Weight { get; set; }
    }
}
