using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioSGP.Domain.Entities
{
    [Table("tarefas")]
    public class Tarefa
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Nome da Tarefa é obrigatório")]
        [MaxLength(100)]
        public string Nome { get; set; }
        [ForeignKey("Projeto")]
        public Guid ProjetoId { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataPrazo { get; set; }
        public string Status { get; set; }
        public Projeto Projeto { get; set; }

        public void SetDataPrazo(DateTime data)
        {
            DataPrazo = data.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(data, DateTimeKind.Utc) : data;
        }
    }
}