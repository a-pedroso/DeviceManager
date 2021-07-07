namespace DeviceManager.Application.Features.Devices.Queries.GetDevicesByBrand
{
    using FluentValidation;

    public class GetDevicesByBrandQueryValidator : AbstractValidator<GetDevicesByBrandQuery>
    {
        public GetDevicesByBrandQueryValidator()
        {
            RuleFor(p => p.Brand)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters.");
        }
    }
}