using System;

namespace Skyly.Domain.Entities
{
    public class AdminRole : BaseEntity
    {
        public Guid AdminId { get; set; }
        public Admin Admin { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}