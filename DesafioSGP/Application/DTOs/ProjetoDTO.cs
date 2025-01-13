using DesafioSGP.Domain.Entities;

public class ProjetoDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public DateOnly? Prazo { get; set; }
    public int UserId { get; set; }
    public List<int> TarefaIds { get; set; } = new List<int>();

    public static ProjetoDTO FromProjeto(Projeto projeto)
    {
        return new ProjetoDTO
        {
            Id = projeto.Id,
            Nome = projeto.Nome,
            Descricao = projeto.Descricao,
            Prazo = projeto.Prazo,
            UserId = projeto.UserId,
            TarefaIds = projeto.Tarefas?.Select(t => t.Id).ToList() ?? new List<int>()
        };
    }
}
