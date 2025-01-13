using AutoMapper;
using DesafioSGP.Application.DTOs;
using DesafioSGP.Domain.Entities;
using DesafioSGP.Domain.Interfaces;

namespace DesafioSGP.Application.Services
{
    public class ProjetoService
    {
        private readonly IProjetoRepository _projetoRepository;
        private readonly IMapper _mapper;

        public ProjetoService(IProjetoRepository projetoRepository, IMapper mapper)
        {
            _projetoRepository = projetoRepository;
            _mapper = mapper;
        }

        public async Task<ProjetoDTO> GetProjetoByIdAsync(int id)
        {
            var projeto = await _projetoRepository.GetByIdAsync(id);
            return _mapper.Map<ProjetoDTO>(projeto);
        }

        public async Task<List<ProjetoDTO>> GetProjetosAsync()
        {
            var projetos = await _projetoRepository.GetAllAsync();
            return _mapper.Map<List<ProjetoDTO>>(projetos);
        }

        public async Task CreateProjetoAsync(ProjetoDTO projetoDto)
        {
            var projeto = _mapper.Map<Projeto>(projetoDto);
            await _projetoRepository.AddAsync(projeto);
        }

        public async Task UpdateProjetoAsync(int id, ProjetoDTO projetoDto)
        {
            var projeto = await _projetoRepository.GetByIdAsync(id);
            if (projeto != null)
            {
                _mapper.Map(projetoDto, projeto);
                await _projetoRepository.UpdateAsync(projeto);
            }
        }

        public async Task DeleteProjetoAsync(int id)
        {
            await _projetoRepository.DeleteAsync(id);
        }
    }
}
