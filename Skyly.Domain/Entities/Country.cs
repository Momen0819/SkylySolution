using System;

namespace Skyly.Domain.Entities
{
    public class Country : BaseEntity
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsActive { get; set; }
    }
}