using System.Threading;

namespace CoWorking.Application.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class
{
    /// <summary>
    /// Returns all entities of the specified type.
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
}
