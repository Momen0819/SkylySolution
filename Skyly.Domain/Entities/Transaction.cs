using System;

namespace Skyly.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string ReferenceNumber { get; set; }
        public bool IsSuccess { get; set; }
    }
}