﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCalorieCounter.Models
{
    public class EditActivityVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayName("Calories burned")]
        public int CaloriesBurned { get; set; }
        [Range(1, 1200)]
        public int Minutes { get; set; }
    }
}
