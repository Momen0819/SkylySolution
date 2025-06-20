using System;

namespace Skyly.Admin.Models
{
    public class HotelViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal CommissionPercentage { get; set; }
    }
}
