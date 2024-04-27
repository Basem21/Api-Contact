using AutoMapper;
using ContactApi.Context;
using ContactApi.Data;
using ContactApi.Interfaces;
using ContactApi.Repositories;
using Microsoft.EntityFrameworkCore;
namespace ContactApi.Context;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<ApplicationDbContext>(Option => Option.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));
        builder.Services.AddScoped<IContacRepository, ContactRepository>();
        builder.Services.AddAutoMapper(typeof(Program));

        var app = builder.Build();
       // builder.Services.AddDbContext<DataContext>(Option => Option.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));

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
