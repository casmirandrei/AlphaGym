using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AlphaGym.Models
{
    public class GymContext : IdentityDbContext<IdentityUser>

    {
        public GymContext(DbContextOptions<GymContext> options)
    : base(options)
        { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Member>? Members { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductID = 1,
                    ProductName = "Basic Subscription",
                    Description = " Access to gym facilities. Entrance until 16:00 & 1 entry per day",
                    ImagePath = "carconvert.png",
                    UnitPrice = 30
                },
                new Product
                {
                    ProductID = 2,
                    ProductName = "Premium subcription",
                    Description = " Access to all facilities and classes. No entry limit",
                    ImagePath = "carearly.png",
                    UnitPrice = 50
                },
                new Product
                {
                    ProductID = 3,
                    ProductName = "Personal trainer subscription",
                    Description = " Access to all facilities and classes. No entry limit. Personalized workouts. Progress tracking and goal setting",
                    ImagePath = "carfast.png",
                    UnitPrice = 100
                }
                
            );

        }
    }
}