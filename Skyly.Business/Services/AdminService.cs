using Dapper;
using Skyly.Business.Interfaces;
using Skyly.Business.Models;
using Skyly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyly.Business.Services
{
    public class AdminService:IAdminService
    {
        private readonly IDbConnection _connectionFactory;
        public AdminService(IDbConnection connection)
        {
            _connectionFactory = connection;
        }
        public async Task<AdminLoginResult> LoginAsync(string username, string password)
        {
            var admin = await _connectionFactory.QueryFirstOrDefaultAsync<Admin>(
                "SELECT * FROM Admins WHERE Username = @Username AND PasswordHash = @Password",
                new { Username = username, Password = password });

            if (admin == null)
                return null;

            var roles = (await _connectionFactory.QueryAsync<string>(
                @"SELECT R.Name FROM AdminRoles AR
          JOIN Roles R ON R.Id = AR.RoleId
          WHERE AR.AdminId = @AdminId", new { AdminId = admin.Id }))
                  .ToList();

            return new AdminLoginResult
            {
                Admin = admin,
                Roles = roles
            };
        }

        public async Task<Admin?> GetByIdAsync(Guid id)
        {
            var sql = "SELECT * FROM Admins WHERE Id = @Id AND IsDeleted = 0";
            var admin = await _connectionFactory.QueryFirstOrDefaultAsync<Admin>(sql, new { Id = id });
            return admin;
        }

        public async Task<bool> UpdateProfileAsync(UpdateAdminProfileDto dto)
        {
            var sql = new StringBuilder();
            sql.Append("UPDATE Admins SET FullName = @FullName, LastModifiedDate = @Now");

            if (!string.IsNullOrWhiteSpace(dto.NewPassword))
                sql.Append(", PasswordHash = @PasswordHash");

            sql.Append(" WHERE Id = @Id AND IsDeleted = 0");

            var parameters = new
            {
                Id = dto.Id,
                FullName = dto.FullName,
                PasswordHash = dto.NewPassword,
                Now = DateTime.UtcNow
            };

            var affected = await _connectionFactory.ExecuteAsync(sql.ToString(), parameters);
            return affected > 0;
        }

    }
}
