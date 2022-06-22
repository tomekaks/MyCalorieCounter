using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Dto
{
    public class DailySumDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUserDto User { get; set; }
        public string Date { get; set; }
        public double Calories { get; set; }
        public double Proteins { get; set; }
        public double Carbs { get; set; }
        public double Fats { get; set; }
        public List<MealDto> Meals { get; set; }
    }
}
