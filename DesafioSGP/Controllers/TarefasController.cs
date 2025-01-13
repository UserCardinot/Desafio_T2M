using DesafioSGP.Application.Services;
using DesafioSGP.Domain.Entities;
using DesafioSGP.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace DesafioSGP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TarefasController : ControllerBase
{
    private readonly TarefaService _tarefaService;
    private readonly IMapper _mapper;

    public TarefasController(TarefaService tarefaService, IMapper mapper)
    {
        _tarefaService = tarefaService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodas()
    {
        var tarefas = await _tarefaService.ObterTodasTarefas();
        return Ok(tarefas);
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

        var tarefa = _tarefaService.CriarTarefaFromDTO(tarefaRequest);

        await _tarefaService.AdicionarTarefa(tarefa);

        return CreatedAtAction(nameof(GetTarefaById), new { id = tarefa.Id }, tarefa);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarTarefa(int id, [FromBody] TarefaPUTDTO tarefaPUTDTO)
    {
        if (tarefaPUTDTO == null)
        {
            return BadRequest("Dados inválidos.");
        }

        var tarefa = _mapper.Map<Tarefa>(tarefaPUTDTO);

        if (tarefa == null)
        {
            return NotFound("Tarefa não encontrada.");
        }

        var tarefaExistente = await _tarefaService.ObterTarefaPorId(id);
        if (tarefaExistente == null)
        {
            return NotFound("Tarefa não encontrada.");
        }

        tarefa.Id = id; // Garantindo que o Id da tarefa seja mantido
        await _tarefaService.AtualizarTarefa(tarefa);

        return Ok("Tarefa atualizada com sucesso.");
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
