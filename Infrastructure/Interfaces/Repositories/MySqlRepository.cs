using MySqlConnector;
using System.Data;
using SoftAPINew.Infrastructure.Interfaces;

namespace SoftAPINew.Infrastructure.Interfaces.Repositories
{
    public class MySqlRepository : IDataRepository
    {
        private readonly string _connectionString = string.Empty;

        public MySqlRepository(string connectionString)
        {
            _connectionString = connectionString ?? string.Empty;
        }

        public async Task<IEnumerable<IDictionary<string, object?>>> GetDataAsync(string storedProc)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IDictionary<string, object?>>> GetDataAsync(string storedProc, IDictionary<string, object?> parameters)
        {
            throw new NotImplementedException();
        }
    }
}