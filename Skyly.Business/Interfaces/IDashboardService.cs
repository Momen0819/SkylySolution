using Skyly.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyly.Business.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardStatsDto> GetStatisticsAsync();
    }
}
