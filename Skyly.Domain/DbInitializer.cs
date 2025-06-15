using Microsoft.EntityFrameworkCore;
using Skyly.Domain.Entities;
using System;
using System.Linq;

namespace Skyly.Domain
{
    public static class DbInitializer
    {
        public static void Seed(SkylyDbContext context)
        {
            context.Database.Migrate();

            if (!context.Admins.Any())
            {
                var admin = new Admin
                {
                    Id = Guid.NewGuid(),
                    Username = "admin",
                    PasswordHash = "admin123", // Replace with hashed password in real case
                    FullName = "Super Admin",
                    Email = "admin@skyly.com",
                    IsActive = true,
                    CreatedBy = "System",
                    LastModifiedBy = "System",
                    LastModifiedDate = DateTime.UtcNow,
                    IsDeleted = false
                };

                context.Admins.Add(admin);

                var roles = new[]
                {
                    "ManageHotels",
                    "ManageRooms",
                    "ManageAdmins",
                    "ManageBlogs",
                    "ManageFAQs",
                    "ViewReports"
                };

                foreach (var roleName in roles)
                {
                    var role = new Role
                    {
                        Id = Guid.NewGuid(),
                        Name = roleName,
                        TitleAr = roleName,
                        TitleEn = roleName,
                        CreatedBy = "System",
                        LastModifiedBy = "System",
                        LastModifiedDate = DateTime.UtcNow,
                        IsDeleted = false
                    };

                    context.Roles.Add(role);

                    context.AdminRoles.Add(new AdminRole
                    {
                        Id = Guid.NewGuid(),
                        AdminId = admin.Id,
                        RoleId = role.Id,
                        CreatedBy = "System",
                        LastModifiedBy = "System",
                        LastModifiedDate = DateTime.UtcNow,
                        IsDeleted = false
                    });
                }

                context.SaveChanges();
            }
        }
    }
}
