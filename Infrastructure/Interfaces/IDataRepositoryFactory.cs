using SoftAPINew.Infrastructure.Interfaces;

public interface IDataRepositoryFactory
{
    IDataRepository Create(string connectionString);


    
}