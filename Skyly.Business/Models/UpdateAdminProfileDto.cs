using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyly.Business.Models
{
    public class UpdateAdminProfileDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string? NewPassword { get; set; }
    }

}
