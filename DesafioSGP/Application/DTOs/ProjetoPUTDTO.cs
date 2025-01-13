namespace DesafioSGP.Application.DTOs
{
    public class ProjetoPUTDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Prazo { get; set; }
        public int UserId { get; set; }
        public List<int> TarefaIds { get; set; } = new List<int>();
    }
}
