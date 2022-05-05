﻿using MyCalorieCounter.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Interfaces.Repositories
{
    public interface IDailySumRepository : IGenericRepository<DailySum>
    {
        Task Update(DailySum dailySum);
    }
}
