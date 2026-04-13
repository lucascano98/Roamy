using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Roamy.Server.Data;
using Roamy.Server.Repositories;

namespace Roamy.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Tell Npgsql to handle unspecified DateTimes as UTC globally
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
                {
                    policy.WithOrigins("https://localhost:7180", "http://localhost:5028").AllowAnyHeader().AllowAnyMethod();
                });
            });
            //Circular reference problem: Trip has a Location list, TripLocation has a Trip navigation property back to Trip, which has Location again.
            //AddJsonOptions to handle cycles
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
            builder.Services.AddScoped<IActivityLocationRepository, ActivityLocationRepository>();
            builder.Services.AddScoped<IDayRepository, DayRepository>();
            builder.Services.AddScoped<ITripRepository, TripRepository>();
            builder.Services.AddScoped<ITripLocationRepository, TripLocationRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapControllers();

            app.Run();
        }
    }
}
