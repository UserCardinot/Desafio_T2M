using DesafioSGP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioSGP.Domain.Interfaces
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> ObterTodos();
        Task<User?> ObterPorId(int id);
        Task<User?> ObterPorEmail(string email);
        Task Adicionar(User user);
        Task Atualizar(User user);
        Task Remover(int id);
    }
}
