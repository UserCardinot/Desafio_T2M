using DesafioSGP.Data;
using DesafioSGP.Domain.Entities;
using DesafioSGP.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DesafioSGP.Infrastructure.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly ApplicationDbContext _context;

        // Injeção de dependência do DbContext
        public TarefaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tarefa>> ObterTodos()
        {
            // Usando o DbContext para obter as tarefas
            return await _context.Tarefas.ToListAsync();
        }

        public async Task<Tarefa?> ObterPorId(int id)
        {
            // Buscando tarefa por id usando o DbContext
            return await _context.Tarefas
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task Adicionar(Tarefa tarefa)
        {
            // Adicionando nova tarefa usando o DbContext
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Tarefa tarefa)
        {
            // Atualizando tarefa usando o DbContext
            _context.Tarefas.Update(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task Remover(int id)
        {
            // Removendo tarefa usando o DbContext
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa != null)
            {
                _context.Tarefas.Remove(tarefa);
                await _context.SaveChangesAsync();
            }
        }
    }
}
