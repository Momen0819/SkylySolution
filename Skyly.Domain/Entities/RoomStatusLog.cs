using System;

namespace Skyly.Domain.Entities
{
    public class RoomStatusLog : BaseEntity
    {
        public Guid RoomId { get; set; }
        public Room Room { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}