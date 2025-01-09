namespace DesafioSGP.Domain.Entities;

public class Tarefa
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Status { get; set; }
    public int ProjetoId { get; set; }
}
