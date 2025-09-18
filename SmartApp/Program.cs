using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SmartApp.Application.Interface;
using SmartApp.Application.Service;
using SmartApp.Infrastructure.BackgroundService;
using SmartApp.Infrastructure.Data;
using SmartApp.Infrastructure.Repository;
using System.Net;
using System.Text.Json.Serialization;
using SendGrid.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using SmartApp.Domain.ModelTemp;
using Microsoft.AspNetCore.Builder;
using SmartApp.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using SmartApp.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>(optional: true)
    .AddEnvironmentVariables();
builder.Services.InjectDbContext(configuration);
builder.Services.AddHttpContextAccessor();
// Add services to the container.
//builder.Services.AddDbContext<RestaurantDbContext>(options
//    => options.UseSqlServer(configuration.GetConnectionString("DbConnection") ?? "")
//    .EnableSensitiveDataLogging()
//    );
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IEmailNotification, EmailNotification>();
builder.Services.AddHostedService<ReminderService>();
builder.Services.AddControllers()
                   .AddJsonOptions(options => {
                       // Ignore self reference loop
                       options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                   });


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Angular dev server
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapOpenApi();
}
app.UseCors("AllowAngularClient");
app.UseHttpsRedirection();


app.MapControllers();



app.Run();
