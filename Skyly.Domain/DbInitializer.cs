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
                var now = DateTime.UtcNow;
                var adminId = Guid.NewGuid();

                var admin = new Admin
                {
                    Id = adminId,
                    Username = "admin",
                    PasswordHash = "admin123", // TODO: استخدم تشفير بعدين
                    FullName = "Super Admin",
                    Email = "admin@skyly.com",
                    IsActive = true,
                    CreatedBy = Guid.Empty,
                    LastModifiedBy = Guid.Empty,
                    LastModifiedDate = now,
                    IsDeleted = false
                };
                context.Admins.Add(admin);

                // ✅ قائمة الصلاحيات
                var roleDefinitions = new (string Name, string TitleAr, string TitleEn)[]
                {
                    // الفنادق
                    ("ManageHotels-View", "عرض الفنادق", "View Hotels"),
                    ("ManageHotels-Add", "إضافة فندق", "Add Hotel"),
                    ("ManageHotels-Edit", "تعديل فندق", "Edit Hotel"),
                    ("ManageHotels-Delete", "حذف فندق", "Delete Hotel"),

                    // الغرف
                    ("ManageRooms-View", "عرض الغرف", "View Rooms"),
                    ("ManageRooms-Add", "إضافة غرفة", "Add Room"),
                    ("ManageRooms-Edit", "تعديل غرفة", "Edit Room"),
                    ("ManageRooms-Delete", "حذف غرفة", "Delete Room"),

                    // المشرفين
                    ("ManageAdmins-View", "عرض المشرفين", "View Admins"),
                    ("ManageAdmins-Add", "إضافة مشرف", "Add Admin"),
                    ("ManageAdmins-Edit", "تعديل مشرف", "Edit Admin"),
                    ("ManageAdmins-Delete", "حذف مشرف", "Delete Admin"),

                    // المدونة
                    ("ManageBlogs-View", "عرض المقالات", "View Blogs"),
                    ("ManageBlogs-Add", "إضافة مقال", "Add Blog"),
                    ("ManageBlogs-Edit", "تعديل مقال", "Edit Blog"),
                    ("ManageBlogs-Delete", "حذف مقال", "Delete Blog"),

                    // الأسئلة الشائعة
                    ("ManageFAQs-View", "عرض الأسئلة الشائعة", "View FAQs"),
                    ("ManageFAQs-Add", "إضافة سؤال", "Add FAQ"),
                    ("ManageFAQs-Edit", "تعديل سؤال", "Edit FAQ"),
                    ("ManageFAQs-Delete", "حذف سؤال", "Delete FAQ"),

                    // التقارير
                    ("ViewReports-Bookings", "تقرير الحجوزات", "View Bookings Report"),
                    ("ViewReports-Rooms", "تقرير الغرف", "View Rooms Report"),
                    ("ViewReports-Hotels", "تقرير الفنادق", "View Hotels Report"),
                    ("ViewReports-Clients", "تقرير العملاء", "View Clients Report"),
                    ("ViewReports-Financial", "تقرير المعاملات المالية", "View Financial Report"),
                };

                // ✅ إدخال الصلاحيات وربطها بالمشرف
                foreach (var (name, titleAr, titleEn) in roleDefinitions)
                {
                    var roleId = Guid.NewGuid();

                    context.Roles.Add(new Role
                    {
                        Id = roleId,
                        Name = name,
                        TitleAr = titleAr,
                        TitleEn = titleEn,
                        CreatedBy = Guid.Empty,
                        LastModifiedBy = Guid.Empty,
                        LastModifiedDate = now,
                        IsDeleted = false
                    });

                    context.AdminRoles.Add(new AdminRole
                    {
                        Id = Guid.NewGuid(),
                        AdminId = adminId,
                        RoleId = roleId,
                        CreatedBy = Guid.Empty,
                        LastModifiedBy = Guid.Empty,
                        LastModifiedDate = now,
                        IsDeleted = false
                    });
                }

                context.SaveChanges();
            }
        }
    }
}
