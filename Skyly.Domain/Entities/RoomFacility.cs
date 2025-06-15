using System;

namespace Skyly.Domain.Entities
{
    public class RoomFacility : BaseEntity
    {
        public Guid RoomId { get; set; }
        public Room Room { get; set; }
        public Guid FacilityId { get; set; }
        public Facility Facility { get; set; }
        public bool IsActive { get; set; }
    }
}