using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyly.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }       // ex: "ManageHotels"
        public string TitleAr { get; set; }    // ex: "إدارة الفنادق"
        public string TitleEn { get; set; }    // ex: "Manage Hotels"
    }
}
