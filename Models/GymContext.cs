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

        public DbSet<Gym>? Gyms { get; set; }
        public DbSet<Member>? Members { get; set; }

    }
}
