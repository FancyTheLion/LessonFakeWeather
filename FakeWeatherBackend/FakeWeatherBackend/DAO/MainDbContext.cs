using Microsoft.EntityFrameworkCore;

namespace FakeWeatherBackend.DAO;

/// <summary>
/// Database context (list of stored objects)
/// </summary>
public class MainDbContext : DbContext
{
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {

    }
}