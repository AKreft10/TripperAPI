using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Entities.Configurations;

namespace TripperAPI.Entities
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DatabaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Place> Places { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new AddressConfiguration().Configure(modelBuilder.Entity<Address>());
            new PhotoConfiguration().Configure(modelBuilder.Entity<Photo>());
            new PlaceConfiguration().Configure(modelBuilder.Entity<Place>());
            new ReviewConfiguration().Configure(modelBuilder.Entity<Review>());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Tripper"));
        }
    }
}
