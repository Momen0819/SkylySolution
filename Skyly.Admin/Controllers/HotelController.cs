using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skyly.Business.Interfaces;
using Skyly.Domain.Entities;
using System;
using System.Security.Claims;

namespace Skyly.Admin.Controllers
{
    [Authorize]
    public class HotelController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly IHttpContextAccessor _contextAccessor;

        public HotelController(IHotelService hotelService, IHttpContextAccessor contextAccessor)
        {
            _hotelService = hotelService;
            _contextAccessor = contextAccessor;
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

        public IActionResult Create()
        {
            if (!HasPermission("ManageHotels-Add"))
                return RedirectToAction("Index", "Dashboard");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Hotel hotel)
        {
            if (!HasPermission("ManageHotels-Add"))
                return RedirectToAction("Index", "Dashboard");

            if (!ModelState.IsValid)
                return View(hotel);

            hotel.CreatedBy = GetAdminId(); // من الجلسة
            _hotelService.Create(hotel);
            return RedirectToAction(nameof(Index));
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
