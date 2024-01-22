using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using ZombieChallenge_OctoCo.Models;
using ZombieChallenge_OctoCo.Services.InventoryItemsService;
using ZombieChallenge_OctoCo.Services.LocationService;
using ZombieChallenge_OctoCo.Services.SurvivorService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<ZombieSurvivorsContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 33))));
builder.Services.AddScoped<ISurvivorService, SurvivorService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
