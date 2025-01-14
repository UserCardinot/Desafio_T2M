using DesafioSGP.Application.DTOs;
using DesafioSGP.Domain.Entities;
using System.ComponentModel.DataAnnotations; // Necessário para a anotação Required

public class ProjetoDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public DateTime? Prazo { get; set; }
    public Guid UserId { get; set; }
    public List<TarefaDTO> Tarefas { get; set; } = new List<TarefaDTO>();

    public static ProjetoDTO FromProjeto(Projeto projeto)
    {
        return new ProjetoDTO
        {
            Id = projeto.Id,
            Nome = projeto.Nome,
            Descricao = projeto.Descricao,
            Prazo = projeto.Prazo?.ToUniversalTime(),
            UserId = projeto.UserId,
            Tarefas = projeto.Tarefas?.Select(t => TarefaDTO.FromTarefa(t)).ToList() ?? new List<TarefaDTO>()
        };
    }
}

