namespace DesafioSGP.Application.DTOs.Auth
{
    public class LoginResponseDTO
    {
        public string Token { get; set; } // O token JWT gerado
        public Guid UserId { get; set; } // ID do usuário autenticado
        public string Email { get; set; } // Email do usuário

        // Construtor opcional para facilitar a inicialização
        public LoginResponseDTO(string token, Guid userId, string email)
        {
            Token = token;
            UserId = userId;
            Email = email;
        }

        // Construtor padrão
        public LoginResponseDTO() { }
    }
}
