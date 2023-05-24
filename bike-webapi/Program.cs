using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using bike_webapi.Data;
using bike_webapi.Interfaces;
using bike_webapi.Repositories;
using bike_webapi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddScoped<IStationRepository, StationRepository>();
builder.Services.AddScoped<IJourneyRepository, JourneyRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

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

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    context.Database.Migrate();

    if (!context.Stations.Any())
    {
        context.Stations.AddRange(
            new List<Station>()
            {
                new Station() 
                { 
                    Id = 5,
                    Nimi = "Jenkruutin asema",
                    Namn = "Jöngruds station",
                    Name = "Jonssons station",
                    Osoite = "Blabla 1",
                    Adress = "Blabla 1",
                    Kaupunki = "Tsadi",
                    Stad = "Tsad",
                    Operaattori = "Veikko Kilkenblad Oy",
                    Kapasiteetti = 5,
                    X = 99.4,
                    Y = 99.9
                },
                new Station()
                { 
                    Id = 6,
                    Nimi = "Kahvila asema",
                    Namn = "kaffe station",
                    Name = "Cafe station",
                    Osoite = "Önsrötinkatu 1",
                    Adress = "Önsrötsgatan 1",
                    Kaupunki = "Epsoo",
                    Stad = "Epsoo",
                    Operaattori = "Veikko Kilkenblad Oy",
                    Kapasiteetti = 5,
                    X = 99.4,
                    Y = 99.9
                }
            }
        );
        context.SaveChanges();
    }
    if (!context.Journeys.Any())
    {
        context.Journeys.AddRange(
            new List<Journey>()
            {
                new Journey()
                {
                    Departure = new DateTime(),
                    Return = new DateTime(),
                    DepartureStationId = 5,
                    ReturnStationId = 6,
                    CoveredDistance = 8393,
                    Duration = 300
                },
                new Journey()
                {
                    Departure = new DateTime(),
                    Return = new DateTime(),
                    DepartureStationId = 6,
                    ReturnStationId = 5,
                    CoveredDistance = 3835,
                    Duration = 120
                }
            }
        );
        context.SaveChanges();
    }
    
}

app.Run();
