using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ASPTurtelMemory.Controllers;
using Newtonsoft;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //dotnet ef dbcontext scaffold "Server=localhost;User=turtle;Password=turtle;Database=turtleoverwatch" "Pomelo.EntityFrameworkCore.MySql" -o Models
        //https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql
        builder.Services.AddDbContext<ASPTurtelMemory.Models.TurtleoverwatchContext>(
            dbContextOptions => dbContextOptions
                .UseMySql("Server=localhost;User=turtle;Password=turtle;Database=turtleoverwatch", new MariaDbServerVersion(new Version(10, 5, 8)))
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
            );


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}