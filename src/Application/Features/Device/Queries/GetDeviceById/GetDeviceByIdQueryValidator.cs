namespace DeviceManager.Application.Features.Devices.Queries.GetDeviceById
{
    using FluentValidation;

    public class GetDeviceByIdQueryValidator : AbstractValidator<GetDeviceByIdQuery>
    {
        public GetDeviceByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                    .WithMessage("{PropertyName} has to be greater than zero.");
        }
    }
}