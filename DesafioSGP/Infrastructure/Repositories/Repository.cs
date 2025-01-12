using Dapper;
using Npgsql;
using System.Data;
using DesafioSGP.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DesafioSGP.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly string _connectionString;
        private IDbConnection Connection => new NpgsqlConnection(_connectionString); // Usando NpgsqlConnection

        public Repository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using var connection = Connection;
            return await connection.QueryAsync<T>($"SELECT * FROM {typeof(T).Name}");
        }

        public async Task<T> GetByIdAsync(int id)
        {
            using var connection = Connection;
            return await connection.QuerySingleOrDefaultAsync<T>($"SELECT * FROM {typeof(T).Name} WHERE Id = @Id", new { Id = id });
        }

        public async Task AddAsync(T entity)
        {
            using var connection = Connection;
            var properties = typeof(T).GetProperties();
            var columns = string.Join(", ", properties.Select(p => p.Name));
            var values = string.Join(", ", properties.Select(p => $"@{p.Name}"));
            var query = $"INSERT INTO {typeof(T).Name} ({columns}) VALUES ({values})";
            await connection.ExecuteAsync(query, entity);
        }

        public async Task UpdateAsync(T entity)
        {
            using var connection = Connection;
            await connection.ExecuteAsync($"UPDATE {typeof(T).Name} SET @Entity WHERE Id = @Id", new { Entity = entity });
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = Connection;
            await connection.ExecuteAsync($"DELETE FROM {typeof(T).Name} WHERE Id = @Id", new { Id = id });
        }
    }
}