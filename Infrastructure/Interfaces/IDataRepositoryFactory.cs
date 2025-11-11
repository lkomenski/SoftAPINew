using SoftAPINew.Infrastructure.Interfaces;

namespace SoftAPINew.Infrastructure.Interfaces
{
    public interface IDataRepositoryFactory
    {
        IDataRepository Create(string connectionString);
    }
}