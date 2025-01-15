using AutoMapper;
using DesafioSGP.Application.DTOs;
using DesafioSGP.Application.Services;
using DesafioSGP.Domain.Entities;
using DesafioSGP.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;

namespace DesafioSGP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetoController : ControllerBase
    {
        private readonly IProjetoRepository _projetoRepository;
        private readonly IMapper _mapper;
        private readonly ProjetoService _projetoService;
        private readonly ApplicationDbContext _context;

        public ProjetoController(IProjetoRepository projetoRepository, IMapper mapper, ProjetoService projetoService, ApplicationDbContext context)
        {
            _projetoRepository = projetoRepository;
            _mapper = mapper;
            _projetoService = projetoService;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CriarProjeto([FromBody] ProjetoDTO projetoDto)
        {
            if (projetoDto == null)
            {
                return BadRequest("Dados inválidos.");
            }

            // Garantindo que as datas sejam salvas como UTC
            var prazoUtc = projetoDto.Prazo.HasValue ? projetoDto.Prazo.Value.ToUniversalTime() : (DateTime?)null;

            var projeto = new Projeto
            {
                Nome = projetoDto.Nome,
                Descricao = projetoDto.Descricao,
                Prazo = prazoUtc,  // Salvando em UTC
                Tarefas = projetoDto.Tarefas.Select(t => new Tarefa
                {
                    Descricao = t.Descricao,
                    DataPrazo = t.DataPrazo.HasValue ? t.DataPrazo.Value.ToUniversalTime() : (DateTime?)null,
                    Status = t.Status,
                    Nome = t.Nome ?? "Tarefa sem nome"
                }).ToList()
            };

            try
            {
                await _context.Projetos.AddAsync(projeto);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(ObterProjeto), new { id = projeto.Id }, projeto);
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"Erro ao acessar o banco de dados: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar o projeto: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Projeto>>> GetAllProjetos()
        {
            // Incluindo as tarefas ao consultar os projetos
            var projetos = await _context.Projetos
                                          .Include(p => p.Tarefas)  // Adiciona o carregamento das tarefas
                                          .ToListAsync();

            if (projetos == null || projetos.Count == 0)
            {
                return NotFound("Nenhum projeto encontrado.");
            }

            return Ok(projetos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterProjeto(Guid id)
        {
            var projeto = await _projetoRepository.GetByIdAsync(id);

            if (projeto == null)
            {
                return NotFound(new { message = "Projeto não encontrado." });
            }

            var projetoDto = _mapper.Map<ProjetoDTO>(projeto);
            return Ok(projetoDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarProjeto(Guid id, ProjetoPUTDTO projetoDto)
        {
            var projetoParaAtualizar = await _projetoRepository.GetByIdAsync(id);

            if (projetoParaAtualizar == null)
            {
                return NotFound(new { message = "Projeto não encontrado para atualização." });
            }

            _mapper.Map(projetoDto, projetoParaAtualizar);
            await _projetoRepository.UpdateAsync(projetoParaAtualizar);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarProjeto(Guid id)
        {
            var projetoExistente = await _projetoRepository.GetByIdAsync(id);
            if (projetoExistente == null)
            {
                return NotFound(new { message = "Projeto não encontrado para exclusão." });
            }

            await _projetoRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
