using DesafioSGP.Domain.Entities;
using DesafioSGP.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DesafioSGP.Application.Services
{
    public class ProjetoService
    {
        private readonly IProjetoRepository _projetoRepository;
        private readonly ApplicationDbContext _context;

        public ProjetoService(IProjetoRepository projetoRepository, ApplicationDbContext context)
        {
            _projetoRepository = projetoRepository;
            _context = context;
        }

        public async Task<List<Projeto>> GetAllProjetosAsync()
        {
            var projetos = await _context.Projetos
                .Include(p => p.Tarefas)
                .ToListAsync();
            return projetos;
        }

        public async Task<Projeto> GetProjetoByIdAsync(Guid id)
        {
            return await _projetoRepository.GetByIdAsync(id);
        }

        public async Task AddProjetoAsync(Projeto projeto)
        {
            await _projetoRepository.AddAsync(projeto);
        }

        public async Task UpdateProjetoAsync(Projeto projeto)
        {
            if (projeto.Prazo.HasValue)
            {
                projeto.Prazo = projeto.Prazo.Value.ToUniversalTime();
            }

            await _projetoRepository.UpdateAsync(projeto);
        }


        public async Task DeleteProjetoAsync(Guid id)
        {
            await _projetoRepository.DeleteAsync(id);
        }
    }
}
