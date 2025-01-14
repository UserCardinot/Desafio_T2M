using DesafioSGP.Application.Services;
using DesafioSGP.Domain.Entities;
using DesafioSGP.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System;
using DesafioSGP.Domain.Interfaces;

namespace DesafioSGP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : ControllerBase
    {
        private readonly TarefaService _tarefaService;
        private readonly IMapper _mapper;

        private readonly ITarefaRepository _tarefaRepository;

        public TarefasController(TarefaService tarefaService, IMapper mapper, ITarefaRepository tarefaRepository)
        {
            _tarefaService = tarefaService;
            _tarefaRepository = tarefaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodas()
        {
            var tarefas = await _tarefaService.ObterTodasTarefas();
            var tarefasDTO = _mapper.Map<List<TarefaDTO>>(tarefas);
            return Ok(tarefasDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TarefaDTO>> GetTarefaById(int id)
        {
            var tarefa = await _tarefaService.ObterTarefaPorId(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            var tarefaDTO = _mapper.Map<TarefaDTO>(tarefa);
            return Ok(tarefaDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] TarefaRequestDTO tarefaRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime? dataPrazoUtc = null;
            if (tarefaRequest.DataPrazo.HasValue)
            {
                dataPrazoUtc = tarefaRequest.DataPrazo.Value.ToUniversalTime(); // Convertendo para UTC
            }

            var tarefa = new Tarefa
            {
                Descricao = tarefaRequest.Descricao,
                ProjetoId = tarefaRequest.ProjetoId,
                DataPrazo = dataPrazoUtc,
                Status = tarefaRequest.Status
            };

            await _tarefaService.AdicionarTarefa(tarefa);

            var tarefaDTO = _mapper.Map<TarefaDTO>(tarefa);
            return CreatedAtAction(nameof(GetTarefaById), new { id = tarefa.Id }, tarefaDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarTarefa(int id, TarefaRequestDTO tarefaRequest)
        {
            if (tarefaRequest.DataPrazo.HasValue)
            {
                tarefaRequest.DataPrazo = tarefaRequest.DataPrazo.Value.ToUniversalTime();
            }

            var tarefaExistente = await _tarefaRepository.ObterPorId(id);
            if (tarefaExistente == null)
            {
                return NotFound();
            }

            tarefaExistente.Descricao = tarefaRequest.Descricao;
            tarefaExistente.ProjetoId = tarefaRequest.ProjetoId;
            tarefaExistente.DataPrazo = tarefaRequest.DataPrazo;
            tarefaExistente.Status = tarefaRequest.Status;

            await _tarefaRepository.Atualizar(tarefaExistente);
            return Ok(tarefaExistente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var tarefaExistente = await _tarefaService.ObterTarefaPorId(id);
            if (tarefaExistente == null)
            {
                return NotFound("Tarefa não encontrada.");
            }

            await _tarefaService.RemoverTarefa(id);
            return NoContent();
        }
    }
}
