using System;
using System.Collections.Generic;

namespace DesafioSGP.Application.DTOs
{
    public class ProjetoPUTDTO
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime? Prazo { get; set; }
        public List<TarefaDTO>? Tarefas { get; set; }
    }
}
