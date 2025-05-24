using CoWorking.Core.Entities;
using System.Threading;

namespace CoWorking.Application.Interfaces.Repositories;

public interface IBookingRepository : IGenericRepository<Booking>
{
    Task CreateAsync(Booking entity, CancellationToken cancellationToken);
    Task<Booking?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task UpdateAsync(Booking entity, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}
