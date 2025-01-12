using AutoMapper;
using DesafioSGP.Application.DTOs;
using DesafioSGP.Domain.Entities;
using DesafioSGP.Domain.Interfaces;

namespace DesafioSGP.Application.Services
{
    public class ProjetoService
    {
        private readonly IRepository<Projeto> _repository;
        private readonly IMapper _mapper;

        public ProjetoService(IRepository<Projeto> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ProjetoDTO>> GetAllAsync()
        {
            var projetos = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProjetoDTO>>(projetos);
        }
    }
}
