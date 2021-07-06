namespace DeviceManager.Application.Features.Devices.Commands.UpdateDevice
{
    using FluentValidation;

    public class UpdateDeviceCommandValidator : AbstractValidator<UpdateDeviceCommand>
    {

        public UpdateDeviceCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0);

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters.");

            RuleFor(p => p.Brand)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters.");
        }
    }
}
