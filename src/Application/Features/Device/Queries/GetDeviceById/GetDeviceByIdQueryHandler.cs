namespace DeviceManager.Application.Features.Devices.Queries.GetDeviceById
{
    using DeviceManager.Application.Common.Exceptions;
    using DeviceManager.Application.Common.Wrappers;
    using DeviceManager.Domain.Entities;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetDeviceByIdQueryHandler : IRequestHandler<GetDeviceByIdQuery, Result<GetDeviceByIdDTO>>
    {
        private readonly IDeviceRepository _deviceRepository;

        public GetDeviceByIdQueryHandler(
            IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<Result<GetDeviceByIdDTO>> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
        {
            var device = await _deviceRepository.GetByIdAsync(request.Id);
            if (device == null)
            {
                throw new NotFoundException(nameof(Device), request.Id);
            }

            var dto = GetDeviceByIdDTO.ToDto(device);

            return Result.Ok(dto);
        }
    }
}