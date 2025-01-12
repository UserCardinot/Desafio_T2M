using Microsoft.AspNetCore.Mvc;
using DesafioSGP.Application.Services;

[ApiController]
[Route("api/[controller]")]
public class ProjetosController : ControllerBase
{
    private readonly ProjetoService _service;

    public ProjetosController(ProjetoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var projetos = await _service.GetAllAsync();
        return Ok(projetos);
    }
}
