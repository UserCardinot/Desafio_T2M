using DesafioSGP.Domain.Entities;

namespace DesafioSGP.Application.DTOs
{
    public class TarefaDTO
    {
        public string Descricao { get; set; }
        public DateTime? Prazo { get; set; }
        public string Status { get; set; }

        public static TarefaDTO FromTarefa(Tarefa tarefa)
        {
            return new TarefaDTO
            {
                Descricao = tarefa.Descricao,
                Prazo = tarefa.DataPrazo,
                Status = tarefa.Status
            };
        }
    }

}
