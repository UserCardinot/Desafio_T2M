using DesafioSGP.Domain.Entities;

namespace DesafioSGP.Domain.Interfaces
{
    public interface IProjetoRepository
    {
        Task<Projeto> GetByIdAsync(int id);
        Task<List<Projeto>> GetAllAsync();
        Task AddAsync(Projeto projeto);
        Task UpdateAsync(Projeto projeto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Projeto>> GetProjetosByUserIdAsync(Guid userId);
    }
}
