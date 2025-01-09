namespace DesafioSGP.Domain.Entities;

public class Users
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public ICollection<Projeto> Projetos { get; set; }
}
