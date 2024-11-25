using LibraryManagementSystem.Application.Service;
using LibraryManagementSystem.Domain.Models.Entities;
using LibraryManagementSystem.Domain.Service;
using LibraryManagementSystem.Infrastructure;
using LibraryManagementSystem.Infrastructure.Context;
using LMS.Core.Models;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureInfrastructure(builder.Configuration);

// Get options
var libraryConfig = builder.Configuration.GetSection(LibraryOptions.SettingName);
builder.Services.Configure<LibraryOptions>(libraryConfig);

//mail settings
var mailSettings = builder.Configuration.GetSection("MailSettings").Get<MailSettings>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

builder.Services.AddCookiePolicy(options =>
{
    options.HttpOnly = HttpOnlyPolicy.Always;
    options.Secure = CookieSecurePolicy.Always;
});

var app = builder.Build();

var serviceScope = app.Services.CreateScope();
var dataContext = serviceScope.ServiceProvider.GetService<LMSDbContext>();
dataContext?.Database.EnsureCreated();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseCors(options =>
{
    options.WithOrigins("http://localhost:5173");
    options.AllowAnyMethod();
    options.AllowAnyHeader();
    options.AllowCredentials();
});
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
