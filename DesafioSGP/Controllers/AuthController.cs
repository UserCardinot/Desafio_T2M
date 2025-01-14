using DesafioSGP.Application.DTOs;
using DesafioSGP.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioSGP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<User> _userManager;

        public AuthController(AuthService authService, ILogger<AuthController> logger, UserManager<User> userManager)
        {
            _authService = authService;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignUpRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { Message = "E-mail e senha são obrigatórios." });
            }

            var normalizedEmail = request.Email.Trim().ToUpperInvariant();
            var userExists = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail);

            if (userExists != null)
            {
                return Conflict(new { Message = "E-mail já registrado." });
            }

            if (request.Password.Length < 6)
            {
                return BadRequest(new { Message = "A senha deve ter pelo menos 6 caracteres." });
            }

            var token = await _authService.RegisterUserAsync(request);
            return Ok(new { Token = token });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            try
            {
                var response = await _authService.LoginUserAsync(request.Email, request.Password);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Tentativa de login inválida.");
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado no login.");
                return StatusCode(500, new { Message = "Erro inesperado no servidor." });
            }
        }

    }
}
