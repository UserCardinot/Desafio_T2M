using DesafioSGP.Domain.Entities;

namespace DesafioSGP.Domain.Interfaces;

public interface ITarefaRepository
{
    Task<IEnumerable<Tarefa>> ObterTodos();
    Task<Tarefa?> ObterPorId(int id);
    Task Adicionar(Tarefa tarefa);
    Task Atualizar(Tarefa tarefa);
    Task Remover(int id);
}