using DesafioSGP.Data;

using DesafioSGP.Domain.Entities;

using DesafioSGP.Domain.Interfaces;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;



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



        public async Task<User?> ObterPorId(int id)

        {

            return await _context.Users.FindAsync(id);

        }



        public async Task<User?> ObterPorEmail(string email)

        {

            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

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



        public async Task Remover(int id)

        {

            var user = await _context.Users.FindAsync(id);



            if (user == null)

            {

                throw new InvalidOperationException("Usuário não encontrado.");

            }



            var projetos = await _projetoRepository.GetProjetosByUserIdAsync(id);



            foreach (var projeto in projetos)

            {

                await _projetoRepository.DeleteAsync(projeto.Id);

            }



            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

        }

    }

}