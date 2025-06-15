using Skyly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyly.Business.Interfaces
{
    public interface IAdminService
    {
        Task<Admin> LoginAsync(string username, string password);
    }
}
