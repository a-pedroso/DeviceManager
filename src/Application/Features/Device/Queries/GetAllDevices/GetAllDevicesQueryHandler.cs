namespace DeviceManager.Application.Features.Devices.Queries.GetAllDevices
{
    using DeviceManager.Application.Common.Wrappers;
    using MediatR;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetAllDevicesQueryHandler : IRequestHandler<GetAllDevicesQuery, Result<IReadOnlyList<GetAllDevicesDTO>>>
    {
        private readonly IDeviceRepository _deviceRepository;
        public GetAllDevicesQueryHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<Result<IReadOnlyList<GetAllDevicesDTO>>> Handle(GetAllDevicesQuery request, CancellationToken cancellationToken)
        {
            var devices = await _deviceRepository.GetAllAsync();

            var response = (IReadOnlyList<GetAllDevicesDTO>)devices.Select(s => GetAllDevicesDTO.ToDto(s)).ToList().AsReadOnly();

            return Result.Ok(response);
        }
    }
}
