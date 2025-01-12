namespace DesafioSGP.Application.DTOs
{
    public class ProjetoDTO
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string? Status { get; set; }
    }
}
