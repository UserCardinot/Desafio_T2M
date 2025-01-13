using DesafioSGP.Domain.Entities;

namespace DesafioSGP.Application.DTOs
{
    public class TarefaDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int ProjetoId { get; set; }
        public DateTime? DataPrazo { get; set; }
        public string Status { get; set; }

        public static TarefaDTO FromTarefa(Tarefa tarefa)
        {
            return new TarefaDTO
            {
                Id = tarefa.Id,
                Descricao = tarefa.Descricao,
                ProjetoId = tarefa.ProjetoId,
                DataPrazo = tarefa.DataPrazo,
                Status = tarefa.Status
            };
        }
    }
}
