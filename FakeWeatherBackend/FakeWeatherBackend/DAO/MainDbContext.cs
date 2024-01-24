using FakeWeatherBackend.DAO.Models;
using Microsoft.EntityFrameworkCore;

namespace FakeWeatherBackend.DAO;

/// <summary>
/// Database context (list of stored objects)
/// </summary>
public class MainDbContext : DbContext
{
    /// <summary>
    /// Weather measurements
    /// </summary>
    public DbSet<WeatherDbo> Weathers { get; set; }
    
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {

    }
}