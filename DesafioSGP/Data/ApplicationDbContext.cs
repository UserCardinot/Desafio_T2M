using DesafioSGP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DesafioSGP.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Projeto> Projetos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Projeto>()
                .Property(p => p.Nome)
                .HasColumnType("VARCHAR(255)");

            modelBuilder.Entity<Projeto>()
                .Property(p => p.Descricao)
                .HasColumnType("TEXT");
        }
    }
}