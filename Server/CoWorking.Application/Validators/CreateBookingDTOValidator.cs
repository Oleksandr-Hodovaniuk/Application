using CoWorking.Application.DTOs.Booking;
using CoWorking.Application.Interfaces.Repositories;
using FluentValidation;

namespace CoWorking.Application.Validators;

public class CreateBookingDTOValidator : AbstractValidator<CreateBookingDTO>
{
    public CreateBookingDTOValidator(IBookingRepository repository)
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

        RuleFor(x => x.WorkspaceType)
            .NotEmpty().WithMessage("Workspace type is required.")
            .Length(2, 60).WithMessage("Workspace type must be between 2 and 60 characters.");

        RuleFor(x => x)
            .Custom((dto, context) => 
            {
                
                var result = dto.WorkspaceType switch
                {
                    "OpenSpace" => 1,
                    "PrivateRoom" => 1,
                    "MeetingRoom" => 1,
                    _ => 0
                };

                if (result == 0)
                {
                    context.AddFailure("WorkspaceType", "Incorrect workspace type.");
                }
            });

        RuleFor(x => x.WorkspaceName)
            .NotEmpty().WithMessage("Workspace name is required.")
            .Length(2, 60).WithMessage("Workspace name must be between 2 and 60 characters.");

        RuleFor(x => x)
            .Custom((dto, context) =>
            {
                var result = dto.WorkspaceName switch
                {
                    "Open Space" => 1,
                    "Private Rooms" => 1,
                    "Meeting Rooms" => 1,
                    _ => 0
                };

                if (result == 0)
                {
                    context.AddFailure("WorkspaceName", "Incorrect workspace name.");
                }
            });

        RuleFor(x => x.StartDateTime)
            .NotEmpty().WithMessage("Start date and time is required.")
            .Must(date => date.Kind != DateTimeKind.Utc).WithMessage("UTC time is not allowed.")
            .GreaterThan(DateTime.Now).WithMessage("Start date and time must be greater than current date and time.");

        RuleFor(x => x.EndDateTime)
            .NotEmpty().WithMessage("End date and time is required.")
            .Must(date => date.Kind != DateTimeKind.Utc).WithMessage("UTC time is not allowed.")
            .GreaterThan(x => x.StartDateTime).WithMessage("End date and time must be greater than start date and time.");

        // Validation for maximum booking duration.
        RuleFor(x => x)
            .Custom((dto, context) =>
            {
                var duration = dto.EndDateTime - dto.StartDateTime;

                var maxDuration = dto.WorkspaceType switch
                {
                    "OpenSpace" => 30,
                    "PrivateRoom" => 30,
                    "MeetingRoom" => 1,
                    _ => 1
                };

                if (duration.TotalDays > maxDuration)
                {
                    context.AddFailure("EndDateTime",
                        $"Maximum booking duration for {dto.WorkspaceType} is {maxDuration} days.");
                }
            });
    }
}
