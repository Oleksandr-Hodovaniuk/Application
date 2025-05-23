using System.Threading;

namespace CoWorking.Application.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
}
