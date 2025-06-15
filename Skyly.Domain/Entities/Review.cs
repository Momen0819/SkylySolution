using System;

namespace Skyly.Domain.Entities
{
    public class Review : BaseEntity
    {
        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public string ClientName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}