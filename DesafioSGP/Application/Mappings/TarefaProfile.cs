using AutoMapper;
using DesafioSGP.Application.DTOs;
using DesafioSGP.Domain.Entities;

namespace DesafioSGP.Application.Mappings
{
    public class TarefaProfile : Profile
    {
        public TarefaProfile()
        {
            CreateMap<Tarefa, TarefaDTO>();
            CreateMap<TarefaRequestDTO, Tarefa>();
        }
    }
}
