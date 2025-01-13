using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioSGP.Domain.Entities
{
    [Table("tarefas")]
    public class Tarefa
    {
        public int Id { get; set; }
        public string Descricao { get; set; }  // Propriedade Descricao
        public int ProjetoId { get; set; }     // Propriedade ProjetoId
        public DateTime? DataPrazo { get; set; } // Propriedade DataPrazo
        public string Status { get; set; }      // Propriedade Status
        public Projeto Projeto { get; set; }    // Relacionamento com a entidade Projeto
    }
}
