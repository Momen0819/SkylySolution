using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skyly.Business.Interfaces;
using Skyly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Skyly.Admin.Controllers
{
    [Authorize]
    public class HotelController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HotelController(IHotelService hotelService, IHttpContextAccessor contextAccessor, IWebHostEnvironment env)
        {
            _hotelService = hotelService;
            _contextAccessor = contextAccessor;
            _webHostEnvironment = env;
        }

        private bool HasPermission(string role) =>
            _contextAccessor.HttpContext?.User?.IsInRole(role) ?? false;

        public IActionResult Index()
        {
            if (!HasPermission("ManageHotels-View"))
                return RedirectToAction("Index", "Dashboard");

            var hotels = _hotelService.GetAll();
            return View(hotels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!HasPermission("ManageHotels-Add"))
                return RedirectToAction("Index", "Dashboard");

            // إرسال الخدمات كلها للـ dropdown
            ViewBag.AllServices = _hotelService.GetAllServices(); 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection form)
        {
            if (!HasPermission("ManageHotels-Add"))
                return RedirectToAction("Index", "Dashboard");

            try
            {
                var hotel = new Hotel
                {
                    Id = Guid.NewGuid(),
                    Name = form["Name"],
                    Address = form["Address"],
                    Phone = form["Phone"],
                    Email = form["Email"],
                    ProfitPercentage = double.Parse(form["ProfitPercentage"]),
                    CreatedBy = GetAdminId(),
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                };

                var username = form["Username"];
                var password = form["Password"];

                var hotelUser = new HotelUser
                {
                    Id = Guid.NewGuid(),
                    HotelId = hotel.Id,
                    Username = username,
                    PasswordHash = password,
                    IsActive = true,
                    CreatedBy = GetAdminId(),
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                };

                // الصور
                var images = new List<HotelImage>();
                var files = Request.Form.Files;
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/hotels");

                // تأكد المجلد موجود
                if (!Directory.Exists(uploadFolder))
                    Directory.CreateDirectory(uploadFolder);

                for (int i = 0; i < files.Count; i++)
                {
                    var file = files[i];
                    if (file.Length == 0) continue;

                    var extension = Path.GetExtension(file.FileName);
                    var fileName = $"{Guid.NewGuid()}{extension}";
                    var filePath = Path.Combine(uploadFolder, fileName);

                    // ترتيب الصورة حسب النموذج
                    int sort = 1;
                    if (int.TryParse(Request.Form[$"images[{i}].sort"], out var parsedSort))
                        sort = parsedSort;

                    // حفظ الصورة في السيرفر
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    images.Add(new HotelImage
                    {
                        Id = Guid.NewGuid(),
                        HotelId = hotel.Id,
                        ImagePath = $"/uploads/hotels/{fileName}", // مسار نسبي
                        Sort = sort,
                        CreatedBy = GetAdminId(),
                        CreatedDate = DateTime.Now,
                        IsDeleted = false
                    });
                }


                // الخدمات
                var services = new List<HotelService>();
                var serviceIds = form["services[].serviceId"];
                var serviceSorts = form["services[].sort"];
                for (int i = 0; i < serviceIds.Count; i++)
                {
                    services.Add(new HotelService
                    {
                        Id = Guid.NewGuid(),
                        HotelId = hotel.Id,
                        ServiceId = Guid.Parse(serviceIds[i]),
                        Sort = int.Parse(serviceSorts[i]),
                        CreatedBy = GetAdminId(),
                        CreatedDate = DateTime.Now,
                        IsDeleted = false
                    });
                }

                // حفظ الكل
                _hotelService.CreateHotelWithDetails(hotel, hotelUser, images, services);

                return RedirectToAction("Index");
            }
            catch
            {
                // إعادة تحميل الصفحة في حالة الخطأ
                ViewBag.AllServices = _hotelService.GetAllServices();
                return View();
            }
        }


        public IActionResult Edit(Guid id)
        {
            if (!HasPermission("ManageHotels-Edit"))
                return RedirectToAction("Index", "Dashboard");

            var hotel = _hotelService.GetById(id);
            if (hotel == null) return NotFound();

            return View(hotel);
        }

        [HttpPost]
        public IActionResult Edit(Hotel hotel)
        {
            if (!HasPermission("ManageHotels-Edit"))
                return RedirectToAction("Index", "Dashboard");

            if (!ModelState.IsValid)
                return View(hotel);

            hotel.LastModifiedBy = GetAdminId(); // من الجلسة
            _hotelService.Update(hotel);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Guid id)
        {
            if (!HasPermission("ManageHotels-Delete"))
                return RedirectToAction("Index", "Dashboard");

            _hotelService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private Guid GetAdminId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
