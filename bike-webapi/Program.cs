using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using bike_webapi.Data;
using bike_webapi.Interfaces;
using bike_webapi.Repositories;
using bike_webapi.Helpers;
using CsvHelper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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

    var dbSeeder = new DatabaseSeeder(context);

    if (!context.Stations.Any())
    {    
        dbSeeder.AddStationsFromCSV("Helsingin_ja_Espoon_kaupunkipyöräasemat_avoin.csv");
    }
    if (!context.Journeys.Any())
    {
        dbSeeder.AddJourneysFromCSV("2021-05.csv");
        dbSeeder.AddJourneysFromCSV("2021-06.csv");
        dbSeeder.AddJourneysFromCSV("2021-07.csv");
    }
    
}

app.Run();
