using AutoMapper;
using DesafioSGP.Application.DTOs;
using DesafioSGP.Domain.Entities;
using DesafioSGP.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioSGP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetoController : ControllerBase
    {
        private readonly IProjetoRepository _projetoRepository;
        private readonly IMapper _mapper;

        public ProjetoController(IProjetoRepository projetoRepository, IMapper mapper)
        {
            _projetoRepository = projetoRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CriarProjeto([FromBody] ProjetoPUTDTO projetoDto)
        {
            if (projetoDto == null)
            {
                return BadRequest("Dados do projeto inválidos.");
            }

            var projeto = _mapper.Map<Projeto>(projetoDto);

            await _projetoRepository.AddAsync(projeto);

            var projetoResponse = _mapper.Map<ProjetoDTO>(projeto);

            return CreatedAtAction(nameof(ObterProjeto), new { id = projeto.Id }, projetoResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterProjeto(int id)
        {
            var projeto = await _projetoRepository.GetByIdAsync(id);

            if (projeto == null)
            {
                return NotFound();
            }

            var projetoDto = _mapper.Map<ProjetoDTO>(projeto);
            return Ok(projetoDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projetos = await _projetoRepository.GetAllAsync();

            if (projetos == null || !projetos.Any())
            {
                return NotFound();
            }

            // Mapear para o DTO de resposta
            var projetosDto = _mapper.Map<IEnumerable<ProjetoDTO>>(projetos);

            return Ok(projetosDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarProjeto(int id, ProjetoPUTDTO projetoDto)
        {
            var projetoParaAtualizar = await _projetoRepository.GetByIdAsync(id);

            if (projetoParaAtualizar == null)
            {
                return NotFound();
            }

            _mapper.Map(projetoDto, projetoParaAtualizar);

            await _projetoRepository.UpdateAsync(projetoParaAtualizar);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarProjeto(int id)
        {
            var projetoExistente = await _projetoRepository.GetByIdAsync(id);
            if (projetoExistente == null)
            {
                return NotFound();
            }

            await _projetoRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
