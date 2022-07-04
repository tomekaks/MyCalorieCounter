using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalorieCounter.Models
{
    public class AddFoodVM
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Calories { get; set; }
        public double Proteins { get; set; }
        public double Carbs { get; set; }
        public double Fats { get; set; }
        [Range(1, 10000)]
        public int Weight { get; set; }
    }
}
