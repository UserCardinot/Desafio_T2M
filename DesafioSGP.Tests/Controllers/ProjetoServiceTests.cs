using Moq;
using Xunit;
using DesafioSGP.Domain.Entities;
using DesafioSGP.Application.Services;
using DesafioSGP.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DesafioSGP.Tests.Controllers
{
    public class ProjetoServiceTests
    {
        private readonly Mock<IProjetoRepository> _mockProjetoRepository;
        private readonly Mock<ApplicationDbContext> _mockDbContext;
        private readonly ProjetoService _projetoService;

        public ProjetoServiceTests()
        {
            // Criando os mocks dos repositórios
            _mockProjetoRepository = new Mock<IProjetoRepository>();

            // Criando um mock do ApplicationDbContext
            _mockDbContext = new Mock<ApplicationDbContext>();

            // Criando o serviço com os mocks
            _projetoService = new ProjetoService(_mockProjetoRepository.Object, _mockDbContext.Object);
        }

        [Fact]
        public async Task AdicionarProjeto_ProjetoValido_DeveAdicionar()
        {
            // Arrange
            var projeto = new Projeto { Id = Guid.NewGuid(), Nome = "Projeto de Teste" };

            // Configurando o mock para o repositório
            _mockProjetoRepository.Setup(repo => repo.AddAsync(It.IsAny<Projeto>())).Returns(Task.CompletedTask);

            // Act
            await _projetoService.AddProjetoAsync(projeto);

            // Assert
            _mockProjetoRepository.Verify(repo => repo.AddAsync(projeto), Times.Once); // Verifica se o método foi chamado
        }

        [Fact]
        public async Task ObterTodosProjetos_DeveRetornarListaDeProjetos()
        {
            // Arrange
            var projetos = new List<Projeto>
            {
                new Projeto { Id = Guid.NewGuid(), Nome = "Projeto 1" },
                new Projeto { Id = Guid.NewGuid(), Nome = "Projeto 2" }
            };

            // Configurando o mock para o DbSet de Projetos no DbContext
            var mockDbSet = new Mock<DbSet<Projeto>>();
            mockDbSet.As<IQueryable<Projeto>>().Setup(m => m.Provider).Returns(projetos.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Projeto>>().Setup(m => m.Expression).Returns(projetos.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Projeto>>().Setup(m => m.ElementType).Returns(projetos.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Projeto>>().Setup(m => m.GetEnumerator()).Returns(projetos.GetEnumerator());

            _mockDbContext.Setup(db => db.Projetos).Returns(mockDbSet.Object);

            // Act
            var result = await _projetoService.GetAllProjetosAsync();

            // Assert
            Assert.Equal(2, result.Count);
            _mockDbContext.Verify(db => db.Projetos, Times.Once);
        }

        [Fact]
        public async Task ObterProjetoPorId_DeveRetornarProjeto()
        {
            // Arrange
            var projetoId = Guid.NewGuid();
            var projeto = new Projeto { Id = projetoId, Nome = "Projeto de Teste" };

            _mockProjetoRepository.Setup(repo => repo.GetByIdAsync(projetoId)).ReturnsAsync(projeto);

            // Act
            var result = await _projetoService.GetProjetoByIdAsync(projetoId);

            // Assert
            Assert.Equal(projetoId, result.Id);
            _mockProjetoRepository.Verify(repo => repo.GetByIdAsync(projetoId), Times.Once);
        }

        [Fact]
        public async Task AtualizarProjeto_ProjetoValido_DeveAtualizar()
        {
            // Arrange
            var projeto = new Projeto { Id = Guid.NewGuid(), Nome = "Projeto de Teste", Prazo = DateTime.Now };

            // Configurando o mock para UpdateAsync
            _mockProjetoRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Projeto>())).Returns(Task.CompletedTask);

            // Act
            await _projetoService.UpdateProjetoAsync(projeto);

            // Assert
            _mockProjetoRepository.Verify(repo => repo.UpdateAsync(projeto), Times.Once);
        }

        [Fact]
        public async Task DeletarProjeto_ProjetoExistente_DeveDeletar()
        {
            // Arrange
            var projetoId = Guid.NewGuid();

            // Configurando o mock para o DeleteAsync
            _mockProjetoRepository.Setup(repo => repo.DeleteAsync(projetoId)).Returns(Task.CompletedTask);

            // Act
            await _projetoService.DeleteProjetoAsync(projetoId);

            // Assert
            _mockProjetoRepository.Verify(repo => repo.DeleteAsync(projetoId), Times.Once);
        }
    }
}
