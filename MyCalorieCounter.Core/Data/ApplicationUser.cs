﻿
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Core.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateJoined { get; set; }
        public List<DailySum> DailySums { get; set; }
        public DailyGoal DailyGoal { get; set; }
    }
}
