namespace DeviceManager.Application.Features.Devices.Commands.CreateDevice
{
    using DeviceManager.Application.Common.Wrappers;
    using DeviceManager.Domain.Entities;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateDeviceCommandHandler : IRequestHandler<CreateDeviceCommand, Result<long>>
    {
        private readonly IDeviceRepository _deviceRepository;

        public CreateDeviceCommandHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<Result<long>> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
        {
            Device device = new()
            {
                Name = request.Name,
                Brand = request.Brand
            };

            await _deviceRepository.AddAsync(device);

            return Result.Ok(device.Id);
        }
    }
}