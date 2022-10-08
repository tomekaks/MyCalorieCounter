using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using MyCalorieCounter.Application.Interfaces.Repositories;
using MyCalorieCounter.Core.Data;
using MyCalorieCounter.Infrastructure.DataBase;
using MyCalorieCounter.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Infrastructure
{
    public static class InfrastructureServicesConfiguration
    {
        public static IServiceCollection ConfigureInfractructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
