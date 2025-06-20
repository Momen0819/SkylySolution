using Dapper;
using Skyly.Business.Interfaces;
using Skyly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyly.Business.Services
{
    public class HotelService : IHotelService
    {
        private readonly IDbConnection _db;

        public HotelService(IDbConnection db)
        {
            _db = db;
        }

        public List<Hotel> GetAll()
        {
            string sql = @"SELECT * FROM Hotels WHERE IsDeleted = 0";
            return _db.Query<Hotel>(sql).ToList();
        }

        public Hotel GetById(Guid id)
        {
            string sql = @"SELECT * FROM Hotels WHERE Id = @id AND IsDeleted = 0";
            return _db.QueryFirstOrDefault<Hotel>(sql, new { id });
        }

        public void Create(Hotel hotel)
        {
            hotel.Id = Guid.NewGuid();
            hotel.CreatedDate = DateTime.Now;

            string sql = @"INSERT INTO Hotels 
                       (Id, Name, Address, Phone, Email, CommissionPercentage, CreatedBy, CreatedDate, IsDeleted)
                       VALUES (@Id, @Name, @Address, @Phone, @Email, @CommissionPercentage, @CreatedBy, @CreatedDate, 0)";
            _db.Execute(sql, hotel);
        }

        public void Update(Hotel hotel)
        {
            hotel.LastModifiedDate = DateTime.Now;

            string sql = @"UPDATE Hotels SET 
                       Name = @Name,
                       Address = @Address,
                       Phone = @Phone,
                       Email = @Email,
                       CommissionPercentage = @CommissionPercentage,
                       LastModifiedBy = @LastModifiedBy,
                       LastModifiedDate = @LastModifiedDate
                       WHERE Id = @Id";
            _db.Execute(sql, hotel);
        }

        public void Delete(Guid id)
        {
            string sql = @"UPDATE Hotels SET IsDeleted = 1, LastModifiedDate = @date WHERE Id = @id";
            _db.Execute(sql, new { id, date = DateTime.Now });
        }
    }

}
