using DesafioSGP.Domain.Entities;

namespace DesafioSGP.Domain.Interfaces
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> ObterTodos();
        Task<User?> ObterPorId(Guid id);
        Task<User?> ObterPorEmail(string email);
        Task<User?> ObterPorNome(string username);
        Task Adicionar(User user);
        Task Atualizar(User user);
        Task Remover(Guid id);
    }
}
