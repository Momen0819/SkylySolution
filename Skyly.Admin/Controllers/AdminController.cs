using Microsoft.AspNetCore.Mvc;
using Skyly.Admin.Models;
using Skyly.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.AspNetCore.Authorization;
using Skyly.Business.Models;

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

            var result = await _adminService.LoginAsync(model.Username, model.Password);
            if (result == null)
            {
                model.ErrorMessage = "بيانات الدخول غير صحيحة";
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, result.Admin.Id.ToString()),
                new Claim(ClaimTypes.Name, result.Admin.FullName),
                new Claim(ClaimTypes.Email, result.Admin.Email ?? "")
            };

            foreach (var role in result.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("tL4m&9eZqB$R1x!WaU7@rM3^NgpXv2#J"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMonths(8),
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // Store the token in a cookie (optional)
            Response.Cookies.Append("SkylyAdminToken", tokenString, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTimeOffset.Now.AddMonths(8)
            });

            return RedirectToAction("Index", "Dashboard");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var admin = await _adminService.GetByIdAsync(Guid.Parse(adminId));

            var vm = new AdminProfileViewModel
            {
                Id = admin.Id,
                FullName = admin.FullName
            };

            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Profile(AdminProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if(model.NewPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError("", "كلمة المرور غير مطابقة");
                return View(model);
            }

            var dto = new UpdateAdminProfileDto
            {
                Id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                FullName = model.FullName,
                NewPassword = model.NewPassword
            };

            var result = await _adminService.UpdateProfileAsync(dto);
            if (!result)
            {
                ModelState.AddModelError("", "حدث خطأ أثناء حفظ البيانات");
                return View(model);
            }

            TempData["Success"] = "تم تحديث الحساب بنجاح";
            return RedirectToAction("Profile");
        }

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
