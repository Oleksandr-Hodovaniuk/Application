using CoWorking.Application.DTOs.AiAssistant;

namespace CoWorking.Application.Interfaces.Services;

public interface IAiAssistantService
{
    Task<string> AskAsync(string question, IEnumerable<AiBookingDTO> bookings, CancellationToken cancellationToken);
}
