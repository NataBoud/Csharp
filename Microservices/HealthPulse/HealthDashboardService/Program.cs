using HealthDashboardService.Application.Services;
using HealthDashboardService.Infrastructure.HttpClients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Health Dashboard API", Version = "v1" });
});

// Configuration des HttpClients avec les URLs des services
var serviceUrls = builder.Configuration.GetSection("ServiceUrls");

builder.Services.AddHttpClient<IPatientServiceClient, PatientServiceClient>(client =>
{
    client.BaseAddress = new Uri(serviceUrls["PatientService"]!);
});

builder.Services.AddHttpClient<IConsultationServiceClient, ConsultationServiceClient>(client =>
{
    client.BaseAddress = new Uri(serviceUrls["ConsultationService"]!);
});

builder.Services.AddHttpClient<IPrescriptionServiceClient, PrescriptionServiceClient>(client =>
{
    client.BaseAddress = new Uri(serviceUrls["PrescriptionService"]!);
});

// Services
builder.Services.AddScoped<IDashboardService, DashboardService>();

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
