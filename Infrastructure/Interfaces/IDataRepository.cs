public interface IDataRepository
{
    Task<IEnumerable<string>> GetDataAsync(string storedProc);
}