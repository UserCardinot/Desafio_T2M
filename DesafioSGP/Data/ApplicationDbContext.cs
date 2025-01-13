using DesafioSGP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DesafioSGP.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Projeto>().ToTable("projetos");
            modelBuilder.Entity<Tarefa>().ToTable("tarefas");
            modelBuilder.Entity<User>().ToTable("users");
        }


    }
}
