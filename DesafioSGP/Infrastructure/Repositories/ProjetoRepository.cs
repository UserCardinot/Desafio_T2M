using DesafioSGP.Data;
using DesafioSGP.Domain.Entities;
using DesafioSGP.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DesafioSGP.Infrastructure.Repositories
{
    public class ProjetoRepository : IProjetoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjetoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Recupera todos os projetos
        public async Task<List<Projeto>> GetAllAsync()
        {
            return await _context.Projetos.ToListAsync();
        }

        // Recupera um projeto pelo ID
        public async Task<Projeto> GetByIdAsync(int id)
        {
            return await _context.Projetos.FindAsync(id);
        }

        // Adiciona um novo projeto
        public async Task AddAsync(Projeto projeto)
        {
            _context.Projetos.Add(projeto);
            await _context.SaveChangesAsync();
        }

        // Atualiza um projeto existente
        public async Task UpdateAsync(Projeto projeto)
        {
            _context.Projetos.Update(projeto);
            await _context.SaveChangesAsync();
        }

        // Deleta um projeto pelo ID
        public async Task DeleteAsync(int id)
        {
            var projeto = await _context.Projetos.FindAsync(id);
            if (projeto != null)
            {
                _context.Projetos.Remove(projeto);
                await _context.SaveChangesAsync();
            }
        }

        // Recupera projetos de um usuário específico
        public async Task<IEnumerable<Projeto>> GetProjetosByUserIdAsync(int userId)
        {
            return await _context.Projetos
                                 .Where(p => p.UserId == userId)  // Filtra os projetos pelo ID do usuário
                                 .ToListAsync();
        }
    }
}
