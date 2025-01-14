using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DesafioSGP.Domain.Entities;
using Microsoft.AspNetCore.Identity;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Projeto> Projetos { get; set; }
    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Definir explicitamente a chave estrangeira de Projeto na Tarefa
        modelBuilder.Entity<Tarefa>()
            .HasOne(t => t.Projeto)
            .WithMany(p => p.Tarefas)
            .HasForeignKey(t => t.ProjetoId)
            .OnDelete(DeleteBehavior.Cascade);  // Defina o comportamento desejado para a exclusão
    }
}