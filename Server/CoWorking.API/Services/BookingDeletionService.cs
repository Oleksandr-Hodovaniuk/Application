using CoWorking.Application.Interfaces.Repositories;
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
                var repository = scope.ServiceProvider.GetRequiredService<IBookingRepository>();

                await repository.DeleteExpiredBookingsAsync(cancellationToken);     
            }    

            await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
        }
    }
}
