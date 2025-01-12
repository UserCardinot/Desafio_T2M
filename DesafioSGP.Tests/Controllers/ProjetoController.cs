using Microsoft.AspNetCore.Mvc;
using DesafioSGP.Application.DTOs;
using DesafioSGP.Application.Interfaces;
using DesafioSGP.Domain.Entities;

namespace DesafioSGP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetoController : ControllerBase
    {
        private readonly IProjetoService _projetoService;

        public ProjetoController(IProjetoService projetoService)
        {
            _projetoService = projetoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjetoDTO>>> GetProjetos()
        {
            var projetos = await _projetoService.GetAllProjetosAsync();
            return Ok(projetos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjetoDTO>> GetProjetoById(int id)
        {
            var projeto = await _projetoService.GetProjetoByIdAsync(id);
            if (projeto == null)
            {
                return NotFound();
            }
            return Ok(projeto);
        }

        [HttpPost]
        public async Task<ActionResult<ProjetoDTO>> CreateProjeto([FromBody] ProjetoDTO projetoDTO)
        {
            if (projetoDTO == null)
            {
                return BadRequest();
            }

            var projetoCriado = await _projetoService.CreateProjetoAsync(projetoDTO);

            return CreatedAtAction(nameof(GetProjetoById), new { id = projetoCriado.Id }, projetoCriado);
        }
    }
}
