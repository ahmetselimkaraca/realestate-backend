using Microsoft.EntityFrameworkCore;
using RealEstateApp.Api.Entity;

namespace RealEstateApp.Api.DatabaseContext
{
    public class RealEstateContext : DbContext
    {
        public RealEstateContext(DbContextOptions<RealEstateContext> options) : base(options)
        {
        }

        public DbSet<RealEstateUser> RealEstateUsers { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Currency> Currencies { get; set; }


    }
}
