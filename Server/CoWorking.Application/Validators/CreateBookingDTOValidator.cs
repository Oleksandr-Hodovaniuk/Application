using CoWorking.Application.DTOs.Booking;
using CoWorking.Application.Interfaces.Repositories;
using FluentValidation;

namespace CoWorking.Application.Validators;

public class CreateBookingDTOValidator : AbstractValidator<CreateBookingDTO>
{
    public CreateBookingDTOValidator(IWorkspaceRepository repository)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(2, 60).WithMessage("Name must be between 2 and 60 characters.")
            .Matches("^[A-Za-zÀ-ÿ' -]+$").WithMessage("Name can only contain letters, spaces, hyphens, and apostrophes.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(x => x.RoomId)
            .NotEmpty().WithMessage("Room id is required.")
            .GreaterThan(0).WithMessage("Room id must be greater than 0");

        RuleFor(x => x.StartDateTime)
            .NotEmpty().WithMessage("Start date and time is required.")
            .Must(date => date.Kind == DateTimeKind.Local).WithMessage("Time must be in local time format.")
            .GreaterThan(DateTime.Now).WithMessage("Start date and time must be greater than current date and time.");

        RuleFor(x => x.EndDateTime)
            .NotEmpty().WithMessage("End date and time is required.")
            .Must(date => date.Kind == DateTimeKind.Local).WithMessage("Time must be in local time format.")
            .GreaterThan(x => x.StartDateTime).WithMessage("End date and time must be greater than start date and time.");

        RuleFor(x => x)
            .CustomAsync(async (dto, context, cancellationToken) => 
            {
                var bookingDuration = (dto.EndDateTime - dto.StartDateTime).TotalDays;

                var maxDuration = await repository.GetWorkspaceMaxDurationAsync(dto.RoomId, cancellationToken );

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
