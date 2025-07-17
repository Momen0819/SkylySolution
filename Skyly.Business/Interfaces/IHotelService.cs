using Skyly.Business.Models;
using Skyly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyly.Business.Interfaces
{
    public interface IHotelService
    {
        List<Hotel> GetAll();
        Hotel GetById(Guid id);
        void Create(Hotel hotel);
        void Update(Hotel hotel);
        void Delete(Guid id);
        List<SimpleServiceDto> GetAllServices();
        void CreateHotelWithDetails(Hotel hotel, HotelUser user, List<HotelImage> images, List<HotelService> services);
    }
}
