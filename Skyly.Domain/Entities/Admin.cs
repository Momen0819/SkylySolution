using System;
using System.Collections.Generic;

namespace Skyly.Domain.Entities
{
    public class Admin : BaseEntity
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}