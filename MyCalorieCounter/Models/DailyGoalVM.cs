using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalorieCounter.Models
{
    public class DailyGoalVM
    {
        public double DailyCaloriesGoal { get; set; }
        public double DailyProteinsGoal { get; set; }
        public double DailyCarbsGoal { get; set; }
        public double DailyFatsGoal { get; set; }
    }
}
