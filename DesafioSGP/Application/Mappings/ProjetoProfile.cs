using AutoMapper;
using DesafioSGP.Application.DTOs;
using DesafioSGP.Domain.Entities;

namespace DesafioSGP.Application.Mappings
{
    public class ProjetoProfile : Profile
    {
        public ProjetoProfile()
        {
            CreateMap<Projeto, ProjetoDTO>();
        }
    }
}
