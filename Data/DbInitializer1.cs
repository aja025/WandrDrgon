using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WanderDragon.Models;

namespace WanderDragon.Data
{
    public class DbInitializer1
    {
        public static void Initialize1(ApplicationDbContext context1)
        {
            context1.Database.EnsureCreated();
            if (!context1.Users.Any())
            {
                AddUsers(context1);
            }
            if (!context1.UserClaims.Any())
            {
                AddUserClaims(context1);
            }
        }

        // Seed db with Admin 
        private static void AddUsers(ApplicationDbContext context1)
        {
            var user = new ApplicationUser
            {
                Id = "e106c376-04b3-4a2d-8ee1-bd498d57c8a6",
                AccessFailedCount = 0,
                ConcurrencyStamp = "4f143dc4-587b-411b-b09a-873532a09c49",
                Email = "admin@wanderdragon.com",
                EmailConfirmed = false,
                LockoutEnabled = true,
                LockoutEnd = null,
                NormalizedEmail = "ADMIN@WANDERDRAGON.COM",
                NormalizedUserName = "ADMIN@WANDERDRAGON.COM",
                PasswordHash = "AQAAAAEAACcQAAAAEE5/0MDVvbpFL13NKFkbeN+ByKi/yiJ6gEd47UKScAUwmq6ZXjw9Pd/cBNLSZaj7eg==",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                SecurityStamp = "032e6fd4-90c3-4dc5-a431-a8a9d14b9ae5",
                TwoFactorEnabled = false,
                UserName = "admin@wanderdragon.com"
            };
            context1.Users.Add(user);
            context1.SaveChanges();
        }

        // Create admin claims
        private static void AddUserClaims(ApplicationDbContext context1)
        {
            var claims = new List<IdentityUserClaim<string>>
            {
                new IdentityUserClaim<string> { Id = 1, ClaimType = "adminId", ClaimValue = "1", UserId = "e106c376-04b3-4a2d-8ee1-bd498d57c8a6" },
                new IdentityUserClaim<string> { Id = 2, ClaimType = "profileId", ClaimValue = "1", UserId = "e106c376-04b3-4a2d-8ee1-bd498d57c8a6" }
            };
            foreach (var claim in claims)
            {
                context1.UserClaims.Add(claim);
            }
            context1.SaveChanges();
        }
    }
}