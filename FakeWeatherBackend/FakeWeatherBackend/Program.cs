using FakeWeatherBackend.Services.Abstract;
using FakeWeatherBackend.Services.Implementations;

namespace FakeWeatherBackend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // DI
        builder.Services.AddScoped<IFakeWeatherService, FakeWeatherService>();
        
        // Add services to the container. 
        
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