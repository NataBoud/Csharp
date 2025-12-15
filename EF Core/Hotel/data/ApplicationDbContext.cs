using Hotel.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.data
{
    internal class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;

        public ApplicationDbContext()
        {
            // Lecture de la connection string depuis appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")  // ou appsettings.Development.json
                .Build();

            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // DbSets
        public DbSet<Client> Clients { get; set; }
        public DbSet<Chambre> Chambres { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configuration pour MySQL
            optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
        }


    }
}
