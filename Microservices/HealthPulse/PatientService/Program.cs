using Microsoft.EntityFrameworkCore;
using PatientService.Application.Services;
using PatientService.Domain.Ports;
using PatientService.Infrastructure.Data;
using PatientService.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("default");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add services to the container
builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Patient Service API", Version = "v1" });
});

// Dependency Injection
// Avant (InMemory)
//builder.Services.AddSingleton<IPatientRepository, InMemoryPatientRepository>();
// Après (EF Core)
builder.Services.AddScoped<IPatientRepository, EfPatientRepository>();
builder.Services.AddScoped<IPatientAppService, PatientAppService>();

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
