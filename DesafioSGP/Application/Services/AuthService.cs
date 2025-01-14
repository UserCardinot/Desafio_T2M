using DesafioSGP.Application.DTOs;
using DesafioSGP.Application.DTOs.Auth;
using DesafioSGP.Domain.Entities;
using DesafioSGP.Infrastructure.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class AuthService
{
    private readonly UserManager<User> _userManager;
    private readonly JwtUtils _jwtUtils;
    private readonly IConfiguration _configuration;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public AuthService(UserManager<User> userManager, JwtUtils jwtUtils, IConfiguration configuration, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _jwtUtils = jwtUtils ?? throw new ArgumentNullException(nameof(jwtUtils));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
    }

    public async Task<string> RegisterUserAsync(SignUpRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request), "A requisição não pode ser nula.");

        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            throw new ArgumentException("O e-mail e a senha são obrigatórios.");

        var normalizedEmail = request.Email.ToUpperInvariant();

        if (string.IsNullOrEmpty(request.Nome))
            throw new ArgumentException("O nome do usuário não pode ser nulo ou vazio.");

        var userExists = await _userManager.Users
            .FirstOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail);

        if (userExists != null)
            throw new InvalidOperationException("E-mail já registrado.");

        var user = new User
        {
            UserName = request.Email,
            Email = request.Email,
            Name = request.Nome,
            NormalizedEmail = normalizedEmail,
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            throw new InvalidOperationException($"Erro ao registrar usuário: {string.Join(", ", result.Errors.Select(e => e.Description))}");

        var role = "User";
        var roleExists = await _roleManager.RoleExistsAsync(role);
        if (!roleExists)
        {
            var roleCreationResult = await _roleManager.CreateAsync(new IdentityRole<Guid>(role));
            if (!roleCreationResult.Succeeded)
                throw new InvalidOperationException("Erro ao criar a role padrão.");
        }

        await _userManager.AddToRoleAsync(user, role);

        // Adiciona um log para depuração
        Console.WriteLine($"Usuário {user.Email} registrado com sucesso.");

        return _jwtUtils.GenerateToken(user);
    }

    public async Task<LoginResponseDTO> LoginUserAsync(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            throw new ArgumentException("E-mail e senha são obrigatórios.");

        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            throw new UnauthorizedAccessException("Usuário ou senha inválidos.");

        var passwordValid = await _userManager.CheckPasswordAsync(user, password);
        if (!passwordValid)
            throw new UnauthorizedAccessException("Usuário ou senha inválidos.");

        var token = _jwtUtils.GenerateToken(user);

        return new LoginResponseDTO
        {
            Token = token,
            UserId = user.Id,
            Email = user.Email
        };
    }
}
