using Dapper;
using Skyly.Business.Interfaces;
using Skyly.Business.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyly.Business.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDbConnection _connection;
        public DashboardService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<DashboardStatsDto> GetStatisticsAsync()
        {
            var stats = new DashboardStatsDto
            {
                HotelsCount = await _connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Hotels WHERE IsDeleted = 0"),
                RoomsCount = await _connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Rooms WHERE IsDeleted = 0"),
                AdminsCount = await _connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Admins WHERE IsDeleted = 0"),
                BookingsCount = await _connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Bookings WHERE IsDeleted = 0")
            };

            return stats;
        }
    }
}
