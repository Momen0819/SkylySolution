using Microsoft.AspNetCore.Mvc;
using Skyly.Admin.Models;
using Skyly.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Skyly.Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var admin = await _adminService.LoginAsync(model.Username, model.Password);
            if (admin == null)
            {
                model.ErrorMessage = "بيانات الدخول غير صحيحة";
                return View(model);
            }

            HttpContext.Session.SetString("AdminId", admin.Id.ToString());
            HttpContext.Session.SetString("AdminName", admin.FullName);

            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
