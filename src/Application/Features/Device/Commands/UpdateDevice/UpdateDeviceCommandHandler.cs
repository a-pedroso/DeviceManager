namespace DeviceManager.Application.Features.Devices.Commands.UpdateDevice
{
    using DeviceManager.Application.Common.Exceptions;
    using DeviceManager.Application.Common.Wrappers;
    using DeviceManager.Domain.Entities;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdateDeviceCommandHandler : IRequestHandler<UpdateDeviceCommand, Result<long>>
    {
        private readonly IDeviceRepository _deviceRepository;

        public UpdateDeviceCommandHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<Result<long>> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
        {
            var device = await _deviceRepository.GetByIdAsync(request.Id);

            if (device == null)
            {
                throw new NotFoundException(nameof(Device), request.Id);
            }
            else
            {
                device.Name = request.Name;
                device.Brand = request.Brand;
                await _deviceRepository.UpdateAsync(device);
                return Result.Ok(device.Id);
            }
        }
    }
}