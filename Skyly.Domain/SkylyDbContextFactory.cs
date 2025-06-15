using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Skyly.Domain
{
    public class SkylyDbContextFactory : IDesignTimeDbContextFactory<SkylyDbContext>
    {
        public SkylyDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SkylyDbContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=SkylyDb;Trusted_Connection=True;TrustServerCertificate=True;");

            return new SkylyDbContext(optionsBuilder.Options);
        }
    }
}
