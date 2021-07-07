namespace DeviceManager.Application.Features.Devices.Commands.CreateDevice
{
    using DeviceManager.Application.Common.Wrappers;
    using MediatR;

    public class CreateDeviceCommand : IRequest<Result<long>>
    {
        public string Name { get; set; }
        public string Brand { get; set; }
    }
}