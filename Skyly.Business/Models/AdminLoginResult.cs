using Skyly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyly.Business.Models
{
    public class AdminLoginResult
    {
        public Admin Admin { get; set; }
        public List<string> Roles { get; set; }
    }
}
