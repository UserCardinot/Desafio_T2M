namespace DesafioSGP.Application.DTOs
{
    public class TarefaRequestDTO
    {
        public string Descricao { get; set; }
        public int ProjetoId { get; set; }
        public DateTime? DataPrazo { get; set; }
        public string Status { get; set; }
    }
}
