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

        public async Task<List<Projeto>> GetAllAsync()
        {
            return await _context.Projetos.ToListAsync();
        }

        public async Task<Projeto> GetByIdAsync(Guid id) // Usando Guid ao invés de int
        {
            return await _context.Projetos.FindAsync(id);
        }

        public async Task AddAsync(Projeto projeto)
        {
            _context.Projetos.Add(projeto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Projeto projeto)
        {
            _context.Projetos.Update(projeto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var projeto = await _context.Projetos.FindAsync(id);
            if (projeto != null)
            {
                _context.Projetos.Remove(projeto);
                await _context.SaveChangesAsync();
            }
        }
    }
}