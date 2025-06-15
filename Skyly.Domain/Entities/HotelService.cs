using System;

namespace Skyly.Domain.Entities
{
    public class HotelService : BaseEntity
    {
        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public Guid ServiceId { get; set; }
        public Facility Service { get; set; }
        public int Sort { get; set; }
        public bool IsActive { get; set; }
    }
}