namespace DeviceManager.Application.Features.Devices.Commands.DeleteDeviceById
{
    using FluentValidation;

    public class DeleteDeviceByIdCommandValidator : AbstractValidator<DeleteDeviceByIdCommand>
    {
        public DeleteDeviceByIdCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0);
        }
    }
}