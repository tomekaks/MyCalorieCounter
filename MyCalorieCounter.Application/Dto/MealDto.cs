using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Dto
{
    public class MealDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUserDto User { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
        public int DailySumId { get; set; }
        public DailySumDto DailySum { get; set; }
        public string Date { get; set; }
        public int Weight { get; set; }
        public double Calories { get; set; }
        public double Proteins { get; set; }
        public double Carbs { get; set; }
        public double Fats { get; set; }
    }
}
