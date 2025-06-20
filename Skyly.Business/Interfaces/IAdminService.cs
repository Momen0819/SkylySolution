using Skyly.Business.Models;
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
        Task<AdminLoginResult> LoginAsync(string username, string password);
        Task<Admin?> GetByIdAsync(Guid id);
        Task<bool> UpdateProfileAsync(UpdateAdminProfileDto dto);
    }
}
