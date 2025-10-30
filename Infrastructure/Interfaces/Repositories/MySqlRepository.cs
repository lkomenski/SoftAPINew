using MySqlConnector;
using System.Data;
using SoftAPINew.Infrastructure.Interfaces;

namespace SoftAPINew.Infrastructure.Interfaces.Repositories.MySql
{
    public class MySqlRepository : IDataRepository
    {
        private readonly string _connectionString;

        public MySqlRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("AP") ?? throw new ArgumentNullException(nameof(config), "DefaultConnection string cannot be null");
        }

        public async Task<IEnumerable<string>> GetDataAsync(string storedProc)
        {
            var data = new List<string>();

            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new MySqlCommand(storedProc, connection);
            command.CommandType = CommandType.StoredProcedure;

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                data.Add(reader.GetString(0));
            }

            return data;
        }
    }
}