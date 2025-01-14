using DesafioSGP.Domain.Entities;
using DesafioSGP.Domain.Interfaces;

namespace DesafioSGP.Application.Services;

public class UserService
{
    private readonly IUsersRepository _userRepository;

    public UserService(IUsersRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> ObterTodosUsuarios()
    {
        return await _userRepository.ObterTodos();
    }

    public async Task<User?> ObterUsuarioPorId(Guid id)
    {
        return await _userRepository.ObterPorId(id);
    }

    public async Task<User?> ObterUsuarioPorEmail(string email)
    {
        return await _userRepository.ObterPorEmail(email);
    }

    public async Task AdicionarUsuario(User user)
    {
        await _userRepository.Adicionar(user);
    }

    public async Task AtualizarUsuario(User user)
    {
        await _userRepository.Atualizar(user);
    }

    public async Task RemoverUsuario(Guid id)
    {
        await _userRepository.Remover(id);
    }
}