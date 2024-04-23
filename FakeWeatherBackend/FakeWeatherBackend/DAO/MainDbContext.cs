using FakeWeatherBackend.DAO.Models;
using FakeWeatherBackend.DAO.Models.Authentification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FakeWeatherBackend.DAO;

/// <summary>
/// Database context (list of stored objects)
/// </summary>
public class MainDbContext : IdentityDbContext<UserDbo, IdentityRole<Guid>, Guid>
{
    /// <summary>
    /// Weather measurements
    /// </summary>
    public DbSet<WeatherDbo> Weathers { get; set; }
    
    /// <summary>
    /// Files
    /// </summary>
    public DbSet<FileDbo> Files { get; set; }
    
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Weather have one photo
        modelBuilder
            .Entity<WeatherDbo>()
            .HasOne(weather => weather.Photo)
            .WithMany(file => file.PhotosForWeathers);

        // Weather have one photo preview
        modelBuilder
            .Entity<WeatherDbo>()
            .HasOne(weather => weather.PhotoPreview)
            .WithMany(file => file.PhotosPreviewsForWeathers);
    }
}