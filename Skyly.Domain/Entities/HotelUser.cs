using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyly.Domain.Entities
{
    public class HotelUser:BaseEntity
    {
        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }   
        public string Username { get; set; }           
        public string PasswordHash { get; set; }      
        public bool IsActive { get; set; }              
    }
}
