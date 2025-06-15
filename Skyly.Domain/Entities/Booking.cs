using System;

namespace Skyly.Domain.Entities
{
    public class Booking : BaseEntity
    {
        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public Guid RoomId { get; set; }
        public Room Room { get; set; }

        public Guid ClientId { get; set; }
        public Client Client { get; set; }

        public string ClientName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int AdultsCount { get; set; }
        public int ChildrenCount { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentStatus { get; set; }
    }
}