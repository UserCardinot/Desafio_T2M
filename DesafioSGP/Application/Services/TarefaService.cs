using System.Globalization;
using DesafioSGP.Application.DTOs;
using DesafioSGP.Domain.Entities;
using DesafioSGP.Domain.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DesafioSGP.Application.Services
{
    public class TarefaService
    {
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IProjetoRepository _projetoRepository;
        private readonly IUsersRepository _userRepository;

        public TarefaService(ITarefaRepository tarefaRepository, IProjetoRepository projetoRepository, IUsersRepository userRepository)
        {
            _tarefaRepository = tarefaRepository;
            _projetoRepository = projetoRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Tarefa>> ObterTodasTarefas()
        {
            return await _tarefaRepository.ObterTodos();
        }

        public async Task<Tarefa?> ObterTarefaPorId(int id)
        {
            return await _tarefaRepository.ObterPorId(id);
        }

        public async Task AdicionarTarefa(Tarefa tarefa)
        {
            await _tarefaRepository.Adicionar(tarefa);
        }

        public async Task AtualizarTarefa(Tarefa tarefa)
        {
            await _tarefaRepository.Atualizar(tarefa);
        }

        public async Task RemoverTarefa(int id)
        {
            await _tarefaRepository.Remover(id);
        }

        public Tarefa CriarTarefaFromDTO(TarefaRequestDTO tarefaRequestDTO)
        {
            DateTime? prazo = null;
            if (DateTime.TryParseExact(tarefaRequestDTO.DataPrazo?.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                prazo = parsedDate;
            }

            var tarefa = new Tarefa
            {
                Id = 0,
                Descricao = tarefaRequestDTO.Descricao,
                ProjetoId = tarefaRequestDTO.ProjetoId,
                DataPrazo = prazo,
                Status = tarefaRequestDTO.Status
            };

            return tarefa;
        }
    }
}
