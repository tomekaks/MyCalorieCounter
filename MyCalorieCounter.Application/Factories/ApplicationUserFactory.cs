using MyCalorieCounter.Application.Dto;
using MyCalorieCounter.Application.Interfaces.Factories;
using MyCalorieCounter.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalorieCounter.Application.Factories
{
    public class ApplicationUserFactory : IApplicationUserFactory
    {
        private readonly IDailyGoalFactory _dailyGoalFactory;
        private readonly IDailySumFactory _dailySumFactory;

        public ApplicationUserFactory(IDailyGoalFactory dailyGoalFactory, IDailySumFactory dailySumFactory)
        {
            _dailyGoalFactory = dailyGoalFactory;
            _dailySumFactory = dailySumFactory;
        }

        public ApplicationUser CreateApplicationUse(ApplicationUserDto applicationUserDto)
        {
            return new ApplicationUser()
            {
                UserName = applicationUserDto.UserName,
                Email = applicationUserDto.Email,
                FirstName = applicationUserDto.FirstName,
                LastName = applicationUserDto.LastName,
                DateJoined = applicationUserDto.DateJoined,
                LockoutEnd = applicationUserDto.LockoutEnd,
                TwoFactorEnabled = applicationUserDto.TwoFactorEnabled,
                PhoneNumberConfirmed = applicationUserDto.PhoneNumberConfirmed,
                PhoneNumber = applicationUserDto.PhoneNumber,
                ConcurrencyStamp = applicationUserDto.ConcurrencyStamp,
                SecurityStamp = applicationUserDto.SecurityStamp,
                PasswordHash = applicationUserDto.PasswordHash,
                EmailConfirmed = applicationUserDto.EmailConfirmed,
                NormalizedEmail = applicationUserDto.NormalizedEmail,
                NormalizedUserName = applicationUserDto.NormalizedUserName,
                LockoutEnabled = applicationUserDto.LockoutEnabled,
                AccessFailedCount = applicationUserDto.AccessFailedCount
            };
        }

        public ApplicationUserDto CreateApplicationUserDto(ApplicationUser applicationUser)
        {
            return new ApplicationUserDto()
            {
                Id = applicationUser.Id,
                UserName = applicationUser.UserName,
                Email = applicationUser.Email,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                DateJoined = applicationUser.DateJoined,
                DailySums = _dailySumFactory.CreateDailySumDtoList(applicationUser.DailySums),
                DailyGoal = _dailyGoalFactory.CreateDailyGoalDto(applicationUser.DailyGoal),
                LockoutEnd = applicationUser.LockoutEnd,
                TwoFactorEnabled = applicationUser.TwoFactorEnabled,
                PhoneNumberConfirmed = applicationUser.PhoneNumberConfirmed,
                PhoneNumber = applicationUser.PhoneNumber,
                ConcurrencyStamp = applicationUser.ConcurrencyStamp,
                SecurityStamp = applicationUser.SecurityStamp,
                PasswordHash = applicationUser.PasswordHash,
                EmailConfirmed = applicationUser.EmailConfirmed,
                NormalizedEmail = applicationUser.NormalizedEmail,
                NormalizedUserName = applicationUser.NormalizedUserName,
                LockoutEnabled = applicationUser.LockoutEnabled,
                AccessFailedCount = applicationUser.AccessFailedCount
            };
        }

        public ApplicationUser MapToModel(ApplicationUser model, ApplicationUserDto dto)
        {
            model.FirstName = dto.FirstName;
            model.LastName = dto.LastName;
            model.PhoneNumber = dto.PhoneNumber;
            model.PasswordHash = dto.PasswordHash;
            model.EmailConfirmed = dto.EmailConfirmed;

            return model;
        }
    }
}
