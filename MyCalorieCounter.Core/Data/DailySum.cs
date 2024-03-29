﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Core.Data
{
    public class DailySum
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public double Calories { get; set; }
        public double Proteins { get; set; }
        public double Carbs { get; set; }
        public double Fats { get; set; }
        public double CaloriesBurned { get; set; }
        public List<Meal> Meals { get; set; }

    }
}
