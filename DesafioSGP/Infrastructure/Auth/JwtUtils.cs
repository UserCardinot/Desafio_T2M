using System.Security.Claims;
using System.Text;
using DesafioSGP.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;

namespace DesafioSGP.Infrastructure.Auth
{
    public class JwtUtils
    {
        private readonly string _secret;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expirationTimeInHours;
        private readonly UserManager<User> _userManager;

        public JwtUtils(IConfiguration configuration, UserManager<User> userManager)
        {
            _secret = configuration["Jwt:Secret"] ?? throw new ArgumentNullException(nameof(configuration), "Chave secreta não configurada.");
            _issuer = configuration["Jwt:Issuer"] ?? throw new ArgumentNullException(nameof(configuration), "Issuer não configurado.");
            _audience = configuration["Jwt:Audience"] ?? throw new ArgumentNullException(nameof(configuration), "Audience não configurada.");
            if (!int.TryParse(configuration["Jwt:ExpirationTimeInHours"], out _expirationTimeInHours))
            {
                throw new ArgumentException("Tempo de expiração do token inválido.", nameof(configuration));
            }

            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager), "UserManager não configurado.");
        }

        public string GenerateToken(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "Usuário não pode ser nulo.");

            if (string.IsNullOrEmpty(user.Email))
                throw new ArgumentException("O e-mail do usuário não pode ser nulo ou vazio.");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            // Adiciona as roles do usuário
            var userRoles = _userManager.GetRolesAsync(user).Result;
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_expirationTimeInHours),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secret);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = _issuer,
                    ValidAudience = _audience
                }, out var validatedToken);

                return principal;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao validar o token: {ex.Message}");
                return null;
            }
        }
    }
}
