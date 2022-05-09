using MyCalorieCounter.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalorieCounter.Models
{
    public class DailyOverviewVM
    {
        public DailyOverviewVM(DailySumDto dailySumDto, DailyGoals dailyGoals)
        {
            Date = dailySumDto.Date;
            Calories = dailySumDto.Calories;
            Proteins = dailySumDto.Proteins;
            Carbs = dailySumDto.Carbs;
            Fats = dailySumDto.Fats;
            DailyCaloriesGoal = dailyGoals.Calories;
            DailyProteinsGoal = dailyGoals.Proteins;
            DailyCarbsGoal = dailyGoals.Carbs;
            DailyFatsGoal = dailyGoals.Fats;
        }
        public string Date { get; set; }
        public double Calories { get; set; }
        public double Proteins { get; set; }
        public double Carbs { get; set; }
        public double Fats { get; set; }
        public double RemainingDailyCalories
        {
            get { return DailyCaloriesGoal - Calories; }
            set { }
        }
        public double RemainingDailyProteins
        {
            get { return DailyProteinsGoal - Proteins; }
            set { }
        }
        public double RemainingDailyCarbs
        {
            get { return DailyCarbsGoal - Carbs; }
            set { }
        }
        public double RemainingDailyFats
        {
            get { return DailyFatsGoal - Fats; }
            set { }
        }
        public double DailyCaloriesGoal { get; set; }
        public double DailyProteinsGoal { get; set; }
        public double DailyCarbsGoal { get; set; }
        public double DailyFatsGoal { get; set; }
    }
}
