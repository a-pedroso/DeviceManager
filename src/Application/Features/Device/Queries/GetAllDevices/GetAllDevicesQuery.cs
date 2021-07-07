namespace DeviceManager.Application.Features.Devices.Queries.GetAllDevices
{
    using DeviceManager.Application.Common.Wrappers;
    using MediatR;
    using System.Collections.Generic;

    public class GetAllDevicesQuery : IRequest<Result<IReadOnlyList<GetAllDevicesDTO>>>
    {
    }
}