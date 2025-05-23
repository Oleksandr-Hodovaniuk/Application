using CoWorking.Core.Entities;

namespace CoWorking.Application.Interfaces.Repositories;

public interface IBookingRepository : IGenericRepository<Booking>
{
    Task DeleteAsync(int id);
}
