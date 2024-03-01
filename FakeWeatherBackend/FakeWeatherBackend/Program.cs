using FakeWeatherBackend.DAO;
using FakeWeatherBackend.DAO.Abstract;
using FakeWeatherBackend.DAO.Implementations;
using FakeWeatherBackend.Mappers.Abstract;
using FakeWeatherBackend.Mappers.Implementations;
using FakeWeatherBackend.Models.Settings;
using FakeWeatherBackend.Services.Abstract;
using FakeWeatherBackend.Services.Implementations;
using Microsoft.EntityFrameworkCore;

namespace FakeWeatherBackend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // DI
        #region DI
            #region Scoped
            
                builder.Services.AddScoped<IWeatherService, WeatherService>();
                builder.Services.AddScoped<IFilesService, FilesService>();

                builder.Services.AddScoped<IWeatherDao, WeatherDao>();
                builder.Services.AddScoped<IFilesDao, FilesDao>();
                
            #endregion
            
            #region Singletons
            
                builder.Services.AddSingleton<IWeatherMapper, WeatherMapper>();
                builder.Services.AddSingleton<IFilesMapper, FilesMapper>();
                
            #endregion
            
        #endregion
        
        #region Settings

        builder.Services.Configure<WeatherValidationSettings>(builder.Configuration.GetSection(nameof(WeatherValidationSettings)));

        #endregion
        
        #region DB Contexts

        // Main
        builder.Services.AddDbContext<MainDbContext>
        (
            options
                =>
                options.UseNpgsql
                (
                    builder.Configuration.GetConnectionString("MainConnection"),
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                ),
            ServiceLifetime.Transient
        );

        #endregion
        
        builder.Services.AddControllers();
        
        #region CORS
        // CORS
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy
            (
                policy =>
                {
                    policy
                        .WithOrigins
                        (
                            "http://localhost:8080"
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                }
            );
        });
        #endregion
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors();
        app.UseAuthorization();
        
        app.MapControllers();

        app.Run();
    }
}