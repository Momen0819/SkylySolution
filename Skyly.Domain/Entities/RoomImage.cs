using System;

namespace Skyly.Domain.Entities
{
    public class RoomImage : BaseEntity
    {
        public Guid RoomId { get; set; }
        public Room Room { get; set; }
        public string ImagePath { get; set; }
        public int Sort { get; set; }
        public bool IsActive { get; set; }
    }
}