namespace DeviceManager.Application.Features.Devices.Queries.GetDevicesByBrand
{
    using DeviceManager.Application.Common.Wrappers;
    using MediatR;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetDevicesByBrandQueryHandler : IRequestHandler<GetDevicesByBrandQuery, Result<IReadOnlyList<GetDevicesByBrandDTO>>>
    {
        private readonly IDeviceRepository _deviceRepository;

        public GetDevicesByBrandQueryHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<Result<IReadOnlyList<GetDevicesByBrandDTO>>> Handle(GetDevicesByBrandQuery request, CancellationToken cancellationToken)
        {
            var devices = await _deviceRepository.GetByBrandAsync(request.Brand);

            var response = (IReadOnlyList<GetDevicesByBrandDTO>)devices.Select(s => GetDevicesByBrandDTO.ToDto(s)).ToList().AsReadOnly();

            return Result.Ok(response);
        }
    }
}