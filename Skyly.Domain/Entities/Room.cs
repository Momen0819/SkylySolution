using System;

namespace Skyly.Domain.Entities
{
    public class Room : BaseEntity
    {
        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public string Name { get; set; }
        public string View { get; set; }
        public int Floor { get; set; }
        public int BedsCount { get; set; }
        public decimal Price { get; set; }
        public string Facilities { get; set; }
        public string Status { get; set; }
    }
}