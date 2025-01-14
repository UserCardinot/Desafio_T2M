using AutoMapper;
using DesafioSGP.Domain.Entities;
using DesafioSGP.Application.DTOs;

namespace DesafioSGP.Application.Mappings
{
    public class ProjetoProfile : Profile
    {
        public ProjetoProfile()
        {
            CreateMap<Projeto, ProjetoDTO>()
                .ForMember(dest => dest.Tarefas, opt => opt.MapFrom(src => src.Tarefas));

            CreateMap<ProjetoDTO, Projeto>()
                .ForMember(dest => dest.Tarefas, opt => opt.MapFrom(src => src.Tarefas));
        }
    }
}
