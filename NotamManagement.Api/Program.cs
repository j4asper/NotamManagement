using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotamManagement.Core.Data;
using NotamManagement.Core.Models;
using NotamManagement.Core.Repository;


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
                _builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
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

        builder.Services.AddAuthorization();
        builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<NotamManagementContext>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.MapIdentityApi<User>();

        app.UseAuthorization();

        app.UseCors("AllowAll");

        app.MapControllers();

        app.Run();
    }
}