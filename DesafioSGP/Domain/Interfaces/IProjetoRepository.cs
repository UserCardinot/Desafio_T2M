using DesafioSGP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioSGP.Domain.Interfaces
{
    public interface IProjetoRepository
    {
        Task<Projeto> GetByIdAsync(Guid id); // Alterando o tipo do ID para Guid
        Task<List<Projeto>> GetAllAsync();
        Task AddAsync(Projeto projeto);
        Task UpdateAsync(Projeto projeto);
        Task DeleteAsync(Guid id);
    }
}