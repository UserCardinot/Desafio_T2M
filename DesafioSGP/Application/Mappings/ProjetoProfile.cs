using AutoMapper;
using DesafioSGP.Domain.Entities;
using DesafioSGP.Application.DTOs;

public class ProjetoProfile : Profile
{
    public ProjetoProfile()
    {
        CreateMap<Projeto, ProjetoDTO>()
            .ForMember(dest => dest.Prazo, opt => opt.MapFrom<PrazoResolver>())
            .ForMember(dest => dest.Tarefas, opt => opt.MapFrom(src => src.Tarefas.Select(t => t.Id).ToList()));

        CreateMap<ProjetoDTO, Projeto>()
            .ForMember(dest => dest.Prazo, opt => opt.MapFrom<PrazoDTOResolver>())
            .ForMember(dest => dest.Tarefas, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

        CreateMap<ProjetoPUTDTO, Projeto>()
            .ForMember(dest => dest.Prazo, opt => opt.MapFrom(src => ConvertStringToDateTime(src.Prazo)))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Tarefas, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }

    public class PrazoResolver : IValueResolver<Projeto, ProjetoDTO, DateTime?>
    {
        public DateTime? Resolve(Projeto source, ProjetoDTO destination, DateTime? destMember, ResolutionContext context)
        {
            return source.Prazo.HasValue ? source.Prazo.Value : (DateTime?)null;
        }
    }

    public class PrazoDTOResolver : IValueResolver<ProjetoDTO, Projeto, DateTime?>
    {
        public DateTime? Resolve(ProjetoDTO source, Projeto destination, DateTime? destMember, ResolutionContext context)
        {
            return source.Prazo.HasValue ? source.Prazo.Value : (DateTime?)null;
        }
    }

    private DateTime? ConvertStringToDateTime(string prazo)
    {
        if (DateTime.TryParse(prazo, out var result))
        {
            return result;
        }

        return null;
    }
}
