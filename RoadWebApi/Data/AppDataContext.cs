using Microsoft.EntityFrameworkCore;
using Road.Api.Models;

namespace Road.Api.Data;
public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }

    public DbSet<Booking> Bookings { get; set; }
}
