using AutoMapper;
using DesafioSGP.Domain.Entities;
using DesafioSGP.Application.DTOs;
using System;

public class ProjetoProfile : Profile
{
    public ProjetoProfile()
    {
        // Mapeamento da entidade Projeto para o DTO ProjetoDTO
        CreateMap<Projeto, ProjetoDTO>()
            .ForMember(dest => dest.Prazo, opt => opt.MapFrom(src => src.Prazo.HasValue ? src.Prazo.Value : (DateOnly?)null))
            .ForMember(dest => dest.TarefaIds, opt => opt.MapFrom(src => src.Tarefas.Select(t => t.Id).ToList()));

        // Mapeamento do DTO ProjetoDTO para a entidade Projeto
        CreateMap<ProjetoDTO, Projeto>()
            .ForMember(dest => dest.Prazo, opt => opt.MapFrom(src => src.Prazo.HasValue ? src.Prazo.Value : (DateOnly?)null))
            .ForMember(dest => dest.Tarefas, opt => opt.Ignore())  // Tarefas são tratadas separadamente
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

        // Mapeamento do ProjetoPUTDTO para Projeto (para atualização)
        CreateMap<ProjetoPUTDTO, Projeto>()
            .ForMember(dest => dest.Prazo, opt => opt.MapFrom(src => ConvertStringToDateOnly(src.Prazo)))  // Usando a função de conversão
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Tarefas, opt => opt.Ignore())  // Ignorando o mapeamento de tarefas
            .ForMember(dest => dest.Id, opt => opt.Ignore());  // Não alterar o ID ao atualizar
    }

    // Função auxiliar para converter a string em DateOnly
    private DateOnly? ConvertStringToDateOnly(string prazo)
    {
        if (DateOnly.TryParse(prazo, out var result))
        {
            return result;
        }

        return null; // Caso a conversão falhe, retorna null
    }
}
