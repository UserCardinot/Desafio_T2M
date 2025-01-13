using DesafioSGP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioSGP.Domain.Interfaces
{
    public interface IProjetoRepository
    {
        Task<Projeto> GetByIdAsync(int id);
        Task<List<Projeto>> GetAllAsync();
        Task AddAsync(Projeto projeto);
        Task UpdateAsync(Projeto projeto);
        Task DeleteAsync(int id);
        Task<IEnumerable<Projeto>> GetProjetosByUserIdAsync(int userId);  // Método adicionado para buscar projetos de um usuário
    }
}
