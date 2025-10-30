using Microsoft.Data.SqlClient;
using System.Data;
using SoftAPINew.Infrastructure.Interfaces;

namespace SoftAPINew.Infrastructure.Interfaces.Repositories.SqlData
{
    public class SqlDataRepository : IDataRepository
    {
        private readonly string _connectionString = string.Empty;

        public SqlDataRepository(string connectionString)
        {
            _connectionString = connectionString ?? string.Empty;
        }

        public async Task<IEnumerable<IDictionary<string, object?>>> GetDataAsync(string storedProc)
        {
            var results = new List<IDictionary<string, object?>>();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(storedProc, connection);
            command.CommandType = CommandType.StoredProcedure;

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var newRow = new Dictionary<string, object?>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    newRow[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                }
                results.Add(newRow);
            }

            return results;
        }

        public async Task<IEnumerable<IDictionary<string, object?>>> GetDataAsync(string storedProc, IDictionary<string, object?> parameters)
        {
            var results = new List<IDictionary<string, object?>>();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(storedProc, connection);
            command.CommandType = CommandType.StoredProcedure;

            // Add parameters to the command
            foreach (var parameter in parameters)
            {
                command.Parameters.AddWithValue($"@{parameter.Key}", parameter.Value ?? DBNull.Value);
            }

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var newRow = new Dictionary<string, object?>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    newRow[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                }
                results.Add(newRow);
            }

            return results;
        }
    }
}