using Microsoft.EntityFrameworkCore;
using Skyly.Domain.Entities;

namespace Skyly.Domain
{
    public class SkylyDbContext : DbContext
    {
        public SkylyDbContext(DbContextOptions<SkylyDbContext> options) : base(options) { }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<AdminRole> AdminRoles { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<HotelImage> HotelImages { get; set; }
        public DbSet<HotelService> HotelServices { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        public DbSet<RoomFacility> RoomFacilities { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<TheView> TheViews { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<WebsiteData> WebsiteData { get; set; }
        public DbSet<RoomStatusLog> RoomStatusLogs { get; set; }
        public DbSet<Facility> Facilities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add any fluent configuration or seeding logic here later
            // منع Cascade Delete في Booking
            modelBuilder.Entity<Booking>()
            .HasOne(b => b.Hotel)
            .WithMany(h => h.Bookings)
            .HasForeignKey(b => b.HotelId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany()
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Client)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}