namespace DeviceManager.Application.Features.Devices.Queries.GetDeviceById
{
    using DeviceManager.Application.Common.Wrappers;
    using MediatR;

    public class GetDeviceByIdQuery : IRequest<Result<GetDeviceByIdDTO>>
    {
        public long Id { get; set; }
    }
}
