using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotamManagement.Core.Data;
using NotamManagement.Core.Models;
using NotamManagement.Core.Repository;
using System.Text;
using NotamManagement.Api.Extensions;
using NotamManagement.Api.Helper;
using NotamManagement.Core.Services;


namespace NotamManagement.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", _builder =>
            {
                _builder.WithOrigins("https://notam-management.jazper.dk").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
            });
        });

        
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<NotamManagementContext>(options =>
        options.UseNpgsql(builder.Configuration.GetRequiredSection("Database:ConnectionString").Value));
        builder.Services.AddScoped<IRepository<Notam>, NotamRepository>();
        builder.Services.AddScoped<IRepository<Airport>, AirportRepository>();
       // builder.Services.AddScoped<IRepository<Coordinates>, CoordinatesRepository>();
        builder.Services.AddScoped<IRepository<FlightPlan>, FlightPlanRepository>();
        builder.Services.AddScoped<IRepository<NotamAction>, NotamActionRepository>();
        builder.Services.AddScoped<IRepository<Organization>, OrganizationRepository>();
        builder.Services.AddScoped<IUserRepository<User>, UserRepository>();
        builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>, CustomUserClaimsPrincipalFactory>();
        builder.Services.AddAuthorization();
        builder.Services.AddSingleton<JwtTokenService>();
        builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<NotamManagementContext>();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "Issuer",
        ValidAudience = "Audience",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("uNUcG9yZThie3MqaG47K2xrR1RLZmFpYzMwRHRtLkR5Yk0="))
    };
});

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors("AllowAll");
        app.MapCustomIdentityApi<User>();
        app.UseAuthentication();
        app.UseAuthorization();
       

        

        app.MapControllers();
        
        app.MapGet("/isAlive", () => "I am alive!");
        
        app.Run();
    }
}