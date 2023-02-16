using CookBook.API.Data;
using CookBook.JWTSecurity.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace CookBook.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseUrls("http://localhost:5250", "http://0.0.0.0:5250");

            // Add services to the container.
            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            builder.Services.AddControllers();

            //Connection String read from appsettings.json
            builder.Services.AddDbContext<cookbookContext>(options =>
            {
                options.UseMySql(builder.Configuration.GetConnectionString("cookbookDB"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.22-mariadb"));
            });

            //Allow Cors
            builder.Services.AddCors(options =>
                options.AddDefaultPolicy(
                policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
            builder.RegisterJWTAuthentication();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            //use Cors
            app.UseCors();

            app.Run();
        }
    }
}