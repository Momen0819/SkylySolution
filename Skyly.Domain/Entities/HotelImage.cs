using System;

namespace Skyly.Domain.Entities
{
    public class HotelImage : BaseEntity
    {
        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public string ImagePath { get; set; }
        public int Sort { get; set; }
        public bool IsActive { get; set; }
    }
}