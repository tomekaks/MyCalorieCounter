using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Core.Data
{
    public class Meal
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int DailySumId { get; set; }
        public DailySum DailySum { get; set; }
        public string Date { get; set; }
        public int Weight { get; set; }
    }
}
