using DesafioSGP.Application.Services;
using DesafioSGP.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DesafioSGP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var usuarios = await _userService.ObterTodosUsuarios();
        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var usuario = await _userService.ObterUsuarioPorId(id);
        if (usuario == null)
        {
            return NotFound();
        }
        return Ok(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar(User user)
    {
        await _userService.AdicionarUsuario(user);
        return CreatedAtAction(nameof(ObterPorId), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(string id, User user)
    {
        if (!Guid.TryParse(id, out Guid guidId) || guidId != user.Id)
        {
            return BadRequest();
        }
        await _userService.AtualizarUsuario(user);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        await _userService.RemoverUsuario(id);
        return NoContent();
    }
    [HttpGet("email/{email}")]
    public async Task<IActionResult> ObterPorEmail(string email)
    {
        var usuario = await _userService.ObterUsuarioPorEmail(email);
        if (usuario == null)
        {
            return NotFound();
        }
        return Ok(usuario);
    }
}