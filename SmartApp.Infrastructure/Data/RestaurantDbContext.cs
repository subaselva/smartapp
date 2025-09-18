using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartApp.Domain.ModelTemp;

namespace SmartApp.Infrastructure.Data

{
    public class RestaurantDbContext : DbContext

    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
            : base(options)

        {

        }
        public DbSet<ContactModel> Contacts { get; set; }
        public DbSet<DiningTable> DiningTables { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantBranch> RestaurantBranches { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
