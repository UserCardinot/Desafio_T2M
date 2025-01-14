using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioSGP.Domain.Entities
{
    [Table("projetos")]
    public class Projeto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O Nome do Projeto é obrigatório")]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A Descrição do Projeto é obrigatória")]
        [MaxLength(500)]
        public string Descricao { get; set; }
        public DateTime? Prazo { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public List<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
    }
}
