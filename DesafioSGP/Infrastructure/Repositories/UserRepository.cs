using DesafioSGP.Domain.Entities;
using DesafioSGP.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DesafioSGP.Infrastructure.Repositories
{
    public class UserRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IProjetoRepository _projetoRepository;

        public UserRepository(ApplicationDbContext context, IProjetoRepository projetoRepository)
        {
            _context = context;
            _projetoRepository = projetoRepository;
        }

        public async Task<IEnumerable<User>> ObterTodos()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> ObterPorId(Guid id)  // Atualizado para Guid
        {
            return await _context.Users.FindAsync(id);  // Busca pelo Guid
        }

        public async Task<User?> ObterPorEmail(string email)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar usuário por email: {ex.Message}");
                throw;
            }
        }

        public async Task<User?> ObterPorNome(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Name == username);
        }

        public async Task Adicionar(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task Remover(Guid id)  // Atualizado para Guid
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new InvalidOperationException("Usuário não encontrado.");
            }

            try
            {
                var projetos = await _projetoRepository.GetProjetosByUserIdAsync(id);

                foreach (var projeto in projetos)
                {
                    await _projetoRepository.DeleteAsync(projeto.Id);
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao remover usuário: {ex.Message}", ex);
            }
        }

    }
}
