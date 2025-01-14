using System;

namespace DesafioSGP.Application.DTOs
{
    public class TarefaDTO
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataPrazo { get; set; }
        public string Status { get; set; }
    }
}
