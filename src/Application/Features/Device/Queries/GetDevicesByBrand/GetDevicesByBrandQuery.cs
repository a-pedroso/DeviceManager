namespace DeviceManager.Application.Features.Devices.Queries.GetDevicesByBrand
{
    using DeviceManager.Application.Common.Wrappers;
    using MediatR;
    using System.Collections.Generic;

    public class GetDevicesByBrandQuery : IRequest<Result<IReadOnlyList<GetDevicesByBrandDTO>>>
    {
        public string Brand { get; set; }
    }
}