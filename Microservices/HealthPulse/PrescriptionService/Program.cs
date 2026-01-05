using Microsoft.EntityFrameworkCore;
using PrescriptionService.Application.Services;
using PrescriptionService.Domain.Ports;
using PrescriptionService.Infrastructure.Data;
using PrescriptionService.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("default");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Consultation Service API", Version = "v1" });
});

// Dependency Injection
builder.Services.AddScoped<IPrescriptionRepository, EfPrescriptionRepository>();
builder.Services.AddScoped<IPrescriptionAppService, PrescriptionAppService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
