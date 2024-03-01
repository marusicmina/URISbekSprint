using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SprintMicroService.Data;
using SprintMicroService.Data.DataPhase;
using SprintMicroService.Entities;
using SprintMicroService.Services.Logger;
using System;
using System.IO;
var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<SprintContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("SprintDB"))
           .LogTo(Console.WriteLine, LogLevel.Information);
});

builder.Services.AddScoped<ISprintRepository, SprintRepository>();
builder.Services.AddScoped<IPhaseRepository, PhaseRepository>();
builder.Services.AddScoped<ILoggerService, LoggerService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.WithOrigins("http://localhost:5278"));
    options.AddPolicy("AllowAngularApp",
            builder => builder.WithOrigins("http://localhost:4200") // replace with your Angular app's address
                              .AllowAnyHeader()


                              .AllowAnyMethod());
});
var app = builder.Build();


app.UseCors("AllowAngularApp");
// Primena CORS politike

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
