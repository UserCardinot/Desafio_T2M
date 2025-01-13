using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioSGP.Domain.Entities
{
    [Table("projetos")]
    public class Projeto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateOnly? Prazo { get; set; }
        public int UserId { get; set; }
        public List<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
    }
}
