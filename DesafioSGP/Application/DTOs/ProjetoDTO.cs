using System;
using System.Collections.Generic;

namespace DesafioSGP.Application.DTOs
{
    public class ProjetoDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime? Prazo { get; set; }
        public Guid UserId { get; set; }
        public List<TarefaDTO> Tarefas { get; set; }
    }
}
