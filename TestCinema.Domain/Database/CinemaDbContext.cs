using Microsoft.EntityFrameworkCore;
using TestCinema.Contracts.Database;

namespace TestCinema.Domain.Database;

public class CinemaDbContext : DbContext
{
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<CinemaHall> CinemaHalls { get; set; }
    public DbSet<CinemaHallSeat> CinemaSeats { get; set; }

    public CinemaDbContext() : base()
    {
    }

    public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("User ID=postgres;Password=H4g1A83;Host=localhost;Port=5432;Database=cinemadb;");
    }
}