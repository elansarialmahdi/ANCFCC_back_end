using Microsoft.EntityFrameworkCore;
using Mohafadati.Services.Titres.Models;
using System.Collections.Generic;

namespace Mohafadati.Services.Titres.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Titre> Titres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Titre>().HasData(new Titre
            {
                Id= 1,
                ConservationFonciere="Rabat",
                NumeroTitre=1234,
                Indice="B1",
                IndiceSpecial="C"
            });

            modelBuilder.Entity<Titre>().HasData(new Titre
            {
                Id = 2,
                ConservationFonciere = "Rabat",
                NumeroTitre = 4321,
                Indice = "D",
                IndiceSpecial = "A3"
            });
        }
    }
}
