using Dapper;
using Skyly.Business.Interfaces;
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
        private readonly IDbConnection _connection;
        public AdminService(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<Admin> LoginAsync(string username, string password)
        {
            var sql = "SELECT * FROM Admins WHERE Username = @Username AND PasswordHash = @Password AND IsDeleted = 0 AND IsActive = 1";
            return await _connection.QueryFirstOrDefaultAsync<Admin>(sql, new { Username = username, Password = password });
        }
    }
}
