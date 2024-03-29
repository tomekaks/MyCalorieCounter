﻿using MyCalorieCounter.Application.Interfaces.Repositories;
using MyCalorieCounter.Core.Data;
using MyCalorieCounter.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Infrastructure.Repositories
{
    public class DailySumRepository : GenericRepository<DailySum>, IDailySumRepository
    {
        private readonly ApplicationDbContext _context;

        public DailySumRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
