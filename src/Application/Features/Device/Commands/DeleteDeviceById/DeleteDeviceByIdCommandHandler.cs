namespace DeviceManager.Application.Features.Devices.Commands.DeleteDeviceById
{
    using DeviceManager.Application.Common.Exceptions;
    using DeviceManager.Application.Common.Wrappers;
    using DeviceManager.Domain.Entities;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeleteDeviceByIdCommandHandler : IRequestHandler<DeleteDeviceByIdCommand, Result>
    {
        private readonly IDeviceRepository _deviceRepository;
        public DeleteDeviceByIdCommandHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }
        public async Task<Result> Handle(DeleteDeviceByIdCommand request, CancellationToken cancellationToken)
        {
            var device = await _deviceRepository.GetByIdAsync(request.Id);
            if (device == null)
            {
                throw new NotFoundException(nameof(Device), request.Id);
            }

            await _deviceRepository.DeleteAsync(device);
            return Result.Ok();
        }
    }
}
