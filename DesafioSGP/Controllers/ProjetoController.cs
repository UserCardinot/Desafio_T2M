using AutoMapper;
using DesafioSGP.Application.DTOs;
using DesafioSGP.Application.Services;
using DesafioSGP.Domain.Entities;
using DesafioSGP.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;  // Adicionado para usar o Include
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
            if (projetoDto == null || string.IsNullOrWhiteSpace(projetoDto.Nome) || string.IsNullOrWhiteSpace(projetoDto.Descricao))
            {
                return BadRequest(new { Message = "Nome e descrição são obrigatórios." });
            }

            if (projetoDto.Prazo.HasValue && projetoDto.Prazo <= DateTime.UtcNow)
            {
                return BadRequest(new { Message = "O prazo deve ser uma data futura." });
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return Unauthorized(new { Message = "Usuário não autenticado ou UserId inválido." });
            }

            var projeto = new Projeto
            {
                Nome = projetoDto.Nome,
                Descricao = projetoDto.Descricao,
                Prazo = projetoDto.Prazo?.ToUniversalTime(),
                UserId = userId
            };

            foreach (var tarefaDto in projetoDto.Tarefas)
            {
                if (string.IsNullOrWhiteSpace(tarefaDto.Descricao))
                {
                    return BadRequest(new { Message = "Tarefas devem ter uma descrição válida." });
                }

                var tarefa = new Tarefa
                {
                    Descricao = tarefaDto.Descricao,
                    DataPrazo = tarefaDto.DataPrazo,
                    Status = tarefaDto.Status ?? "Pendente",
                    Projeto = projeto
                };

                projeto.Tarefas.Add(tarefa);
            }

            _context.Projetos.Add(projeto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("ObterProjeto", new { id = projeto.Id }, projeto);
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
