using AutoMapper;
using DesafioSGP.Domain.Entities;
using DesafioSGP.Application.DTOs;
using System;

public class ProjetoProfile : Profile
{
    public ProjetoProfile()
    {
        CreateMap<Projeto, ProjetoDTO>()
            .ForMember(dest => dest.Prazo, opt => opt.MapFrom(src => src.Prazo.HasValue ? src.Prazo.Value : (DateOnly?)null))
            .ForMember(dest => dest.TarefaIds, opt => opt.MapFrom(src => src.Tarefas.Select(t => t.Id).ToList()));

        CreateMap<ProjetoDTO, Projeto>()
            .ForMember(dest => dest.Prazo, opt => opt.MapFrom(src => src.Prazo.HasValue ? src.Prazo.Value : (DateOnly?)null))
            .ForMember(dest => dest.Tarefas, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

        CreateMap<ProjetoPUTDTO, Projeto>()
            .ForMember(dest => dest.Prazo, opt => opt.MapFrom(src => ConvertStringToDateOnly(src.Prazo)))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Tarefas, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }

    private DateOnly? ConvertStringToDateOnly(string prazo)
    {
        if (DateOnly.TryParse(prazo, out var result))
        {
            return result;
        }

        return null;
    }
}
