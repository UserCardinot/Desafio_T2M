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

        // Método para obter todas as tarefas
        public async Task<IEnumerable<Tarefa>> ObterTodasTarefas()
        {
            return await _tarefaRepository.ObterTodos();
        }

        // Método para obter uma tarefa por ID
        public async Task<Tarefa?> ObterTarefaPorId(int id)
        {
            return await _tarefaRepository.ObterPorId(id);
        }

        // Método para adicionar uma nova tarefa
        public async Task AdicionarTarefa(Tarefa tarefa)
        {
            await _tarefaRepository.Adicionar(tarefa);
        }

        // Método para atualizar uma tarefa existente
        public async Task AtualizarTarefa(Tarefa tarefa)
        {
            await _tarefaRepository.Atualizar(tarefa);
        }

        // Método para remover uma tarefa por ID
        public async Task RemoverTarefa(int id)
        {
            await _tarefaRepository.Remover(id);
        }

        // Método para criar a tarefa a partir do DTO
        public Tarefa CriarTarefaFromDTO(TarefaRequestDTO tarefaRequestDTO)
        {
            // Verifica se a data de prazo está no formato correto antes de converter
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
