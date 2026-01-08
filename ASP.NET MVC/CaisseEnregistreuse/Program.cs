using CaisseEnregistreuse.Data;
using CaisseEnregistreuse.Services;
using CaisseEnregistreuse.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// HttpContextAccessor (déjà présent)
builder.Services.AddHttpContextAccessor();

// SESSION CONFIGURATION
builder.Services.AddDistributedMemoryCache(); // Nécessaire pour stocker la session en mémoire
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // durée de vie de la session
    options.Cookie.HttpOnly = true;                 // sécurité cookie
    options.Cookie.IsEssential = true;             // nécessaire même si l'utilisateur refuse les cookies
});


// Configure Entity Framework with MySQL
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("La chaîne de connexion 'DefaultConnection' est manquante ou vide dans la configuration.");
}
builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


// Enregistrement des service
builder.Services.AddScoped<ICategorieService, CategorieService>();
builder.Services.AddScoped<IProduitService, ProduitService>();
builder.Services.AddScoped<IPanierService, PanierService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Produit/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// SESSION MIDDLEWARE
app.UseAuthorization();
app.UseSession(); // doit être après UseRouting mais avant UseEndpoints ou MapControllerRoute

// Middleware temporaire pour debug de la session
app.Use(async (context, next) =>
{
    var panierJson = context.Session.GetString("Panier"); // récupère la session "Panier"
    Console.WriteLine($"Panier actuel : {panierJson ?? "vide"}");
    await next.Invoke(); // continue la pipeline
});

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Produit}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
