using System;

namespace Skyly.Domain.Entities
{
    public class City : BaseEntity
    {
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsActive { get; set; }
    }
}