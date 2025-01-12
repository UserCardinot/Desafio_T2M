namespace DesafioSGP.Domain.Entities
{
    public class Tarefa
    {
        public required int Id { get; set; }
        public required int ProjetoId { get; set; }
        public required string Nome { get; set; }
        public required string Descricao { get; set; }
        public string? Status { get; set; }
        public required DateTime DataPrazo { get; set; }
    }
}
