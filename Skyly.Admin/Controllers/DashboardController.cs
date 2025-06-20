using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skyly.Business.Interfaces;
using System.Threading.Tasks;

namespace Skyly.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var stats = await _dashboardService.GetStatisticsAsync();
            return View(stats);
        }
    }
}
