using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioSGP.Domain.Entities
{
    [Table("tarefas")]
    public class Tarefa
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int ProjetoId { get; set; }
        public DateTime? DataPrazo { get; set; }
        public string Status { get; set; }
        public Projeto Projeto { get; set; }
    }
}
