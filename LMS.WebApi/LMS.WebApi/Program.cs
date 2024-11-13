using LMS.Core.Models;
using LMS.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var libraryConfig = builder.Configuration.GetSection(LibraryOptions.SettingName);
builder.Services.Configure<LibraryOptions>(libraryConfig);

builder.Services.ConfigureInfrastructure(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
