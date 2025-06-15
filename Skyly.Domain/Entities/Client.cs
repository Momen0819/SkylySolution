using System;
using System.Collections.Generic;

namespace Skyly.Domain.Entities
{
    public class Client : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}