using Microsoft.AspNetCore.Identity;

namespace DesafioSGP.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public ICollection<Projeto> Projetos { get; set; }
    }
}
