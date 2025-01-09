namespace DesafioSGP.Domain.Entities;

public class Projeto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public DateTime Prazo { get; set; }
    public int UsersId { get; set; }
    public Users Users { get; set; }
}
