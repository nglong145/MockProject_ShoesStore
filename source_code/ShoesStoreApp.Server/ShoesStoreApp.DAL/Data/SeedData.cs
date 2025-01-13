using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.DAL.Data
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Seed roles
            var adminRoleId = Guid.NewGuid();
            var userRoleId = Guid.NewGuid();

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new Role
                {
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName = "USER"
                }
            );

            // Seed admin user
            var adminId = Guid.NewGuid();
            var passwordHasher = new PasswordHasher<User>();

            var adminUser = new User
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                RoleId = adminRoleId,
                Address = "Default Address",
                FullName = "Administrator"
            };

            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin@123");

            modelBuilder.Entity<User>().HasData(adminUser);

           
        }
    }
}
