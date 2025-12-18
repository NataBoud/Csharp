using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WasteMicroservice.Application.Service;
using WasteMicroservice.Domain.Ports;
using WasteMicroservice.Infrastructure.Data;
using WasteMicroservice.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddScoped<IWasteRepository, WasteRepository>();
builder.Services.AddScoped<IWasteService, WasteService>();


string connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();