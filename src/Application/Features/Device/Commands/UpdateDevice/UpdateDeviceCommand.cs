namespace DeviceManager.Application.Features.Devices.Commands.UpdateDevice
{
    using DeviceManager.Application.Common.Wrappers;
    using MediatR;

    public class UpdateDeviceCommand : IRequest<Result<long>>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
    }
}