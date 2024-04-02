using System.Text;
using FakeWeatherBackend.DAO;
using FakeWeatherBackend.DAO.Abstract;
using FakeWeatherBackend.DAO.Implementations;
using FakeWeatherBackend.DAO.Models.Authentification;
using FakeWeatherBackend.Mappers.Abstract;
using FakeWeatherBackend.Mappers.Implementations;
using FakeWeatherBackend.Models.Settings;
using FakeWeatherBackend.Services.Abstract;
using FakeWeatherBackend.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
                builder.Services.AddScoped<IResizeService, ResizeService>();

                builder.Services.AddScoped<IWeatherDao, WeatherDao>();
                builder.Services.AddScoped<IFilesDao, FilesDao>();

                builder.Services.AddScoped<IAccountsService, AccountsService>();
                
            #endregion
            
            #region Singletons
            
                builder.Services.AddSingleton<IWeatherMapper, WeatherMapper>();
                builder.Services.AddSingleton<IFilesMapper, FilesMapper>();
                
            #endregion
            
        #endregion
        
        #region Settings

        builder.Services.Configure<WeatherValidationSettings>(builder.Configuration.GetSection(nameof(WeatherValidationSettings)));
        builder.Services.Configure<PhotoSizeSettings>(builder.Configuration.GetSection(nameof(PhotoSizeSettings)));
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));

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
        
        #region Identity framework

        // Identity
        builder.Services.AddIdentity<UserDbo, IdentityRole<Guid>>()  
            .AddEntityFrameworkStores<MainDbContext>()  
            .AddDefaultTokenProviders();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            // Password settings
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequiredUniqueChars = 4;
        });

        // JWT settings
        var jwtSettings = builder
            .Configuration
            .GetSection(nameof(JwtSettings))
            .Get<JwtSettings>();

        builder.Services.AddAuthentication(options =>  
            {  
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;  
            })  
        
            // Adding Jwt Bearer  
            .AddJwtBearer(options =>  
                {  
                    options.SaveToken = true;  
                    options.RequireHttpsMetadata = false;  
                    options.TokenValidationParameters = new TokenValidationParameters()  
                    {  
                        ValidateIssuer = true,  
                        ValidateAudience = true,  
                        ValidAudience = jwtSettings.ValidAudience,  
                        ValidIssuer = jwtSettings.ValidIssuer,  
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))  
                    };  
                }
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
        builder.Services.AddSwaggerGen
        (
            sc =>
            {
                sc.SwaggerDoc("v1", new OpenApiInfo() { Title = "Weather app API", Version = "v1" });
        
                sc.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = @"JWT Authorization token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
        
                sc.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            }
        );

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseCors();
        app.UseAuthorization();
        
        app.MapControllers();

        app.Run();
    }
}