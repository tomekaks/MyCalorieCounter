using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Interfaces.Factories
{
    public interface IApplicationUserFactory
    {
        ApplicationUser CreateApplicationUse(ApplicationUserDto applicationUserDto);
        ApplicationUserDto CreateApplicationUserDto(ApplicationUser applicationUser);
        ApplicationUser MapToModel(ApplicationUser model, ApplicationUserDto dto);
    }
}
