using CoWorking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoWorking.API.Services;

internal class BookingDeletionService : BackgroundService
{
    private readonly IServiceProvider _services;
    public BookingDeletionService(IServiceProvider services)
    {
        _services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            using (var scope = _services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<CoWorkingDbContext>();

                var now = DateTime.Now;

                var expiredBookings = await dbContext.Bookings
                    .Where(b => b.EndDateTime <= now)
                    .ToListAsync(cancellationToken);

                dbContext.Bookings.RemoveRange(expiredBookings);
                await dbContext.SaveChangesAsync(cancellationToken);
            }    

            await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
        }
    }
}
