using CoWorking.Application.DTOs.Booking;
using CoWorking.Application.Interfaces.Repositories;
using FluentValidation;

namespace CoWorking.Application.Validators;

public class PatchBookingDTOValidator : AbstractValidator<PatchBookingDTO>
{
    public PatchBookingDTOValidator(IWorkspaceRepository repository)
    {
        RuleFor(x => x.Name)
             .NotEmpty().WithMessage("Name is required.")
             .Length(2, 60).WithMessage("Name must be between 2 and 60 characters.")
             .Matches("^[A-Za-zÀ-ÿ' -]+$").WithMessage("Name can only contain letters, spaces, hyphens, and apostrophes.")
             .When(x => x.Name is not null);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Invalid email format.")
            .When(x => x.Email is not null);

        RuleFor(x => x.SelectedRoomId)
            .NotEmpty().WithMessage("Room id is required.")
            .GreaterThan(0).WithMessage("Room id must be greater than 0")
            .When(x => x.SelectedRoomId.HasValue);

        RuleFor(x => x.StartDateTime)
           .NotEmpty().WithMessage("Start date and time is required.")
           .GreaterThan(DateTime.Now).WithMessage("Start date and time must be greater than current date and time.")
           .When(x => x.StartDateTime.HasValue);

        RuleFor(x => x.EndDateTime)
            .NotEmpty().WithMessage("End date and time is required.")
            .GreaterThan(x => x.StartDateTime).WithMessage("End date and time must be greater than start date and time.")
            .When(x => x.EndDateTime.HasValue && x.StartDateTime.HasValue);

        RuleFor(x => x)
            .CustomAsync(async (dto, context, cancellationToken) =>
            {
                if (dto.SelectedRoomId is null || dto.StartDateTime is null || dto.EndDateTime is null)
                {
                    return;
                }
                  
                var bookingDuration = (dto.EndDateTime.Value - dto.StartDateTime.Value).TotalDays;

                var maxDuration = await repository.GetWorkspaceMaxDurationAsync(dto.SelectedRoomId.Value, cancellationToken);

                if (maxDuration == null)
                {
                    context.AddFailure("RoomId", "Unable to determine maximum booking duration for this workspace.");
                    return;
                }

                if (bookingDuration > maxDuration)
                {
                    context.AddFailure("EndDateTime",
                        $"Maximum booking duration for this workspace is {maxDuration} day(s).");
                }
            });  
    }
}
